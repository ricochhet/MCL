/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricLoaderDownloadService : ILoaderDownloadService<FabricUrls>, IDownloadService
{
    public static FabricVersionManifest FabricVersionManifest { get; private set; }
    public static FabricProfile FabricProfile { get; private set; }
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static LauncherInstance _launcherInstance;
    private static FabricUrls _fabricUrls;
    private static bool _loaded = false;

    public static void Init(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        FabricUrls fabricUrls
    )
    {
        if (ObjectValidator<LauncherInstance>.IsNull(launcherInstance))
            return;

        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherInstance = launcherInstance;
        _fabricUrls = fabricUrls;
        _loaded = true;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!_loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!await DownloadProfile())
            return false;

        if (!LoadProfile())
            return false;

        if (!LoadLoaderVersion())
            return false;

        if (!await DownloadLoader())
            return false;

        return true;
    }

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await FabricVersionManifestDownloader.Download(_launcherPath, _fabricUrls))
        {
            NotificationService.Error("error.download", nameof(FabricVersionManifestDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricVersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(FabricVersionManifest));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricVersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!_loaded)
            return false;

        if (!await FabricProfileDownloader.Download(_launcherPath, _launcherVersion, _fabricUrls))
        {
            NotificationService.Error("error.download", nameof(FabricProfileDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!_loaded)
            return false;

        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [_launcherVersion?.Version, _launcherVersion?.FabricLoaderVersion]
            )
        )
            return false;

        FabricProfile = Json.Load<FabricProfile>(FabricPathResolver.ProfilePath(_launcherPath, _launcherVersion));
        if (ObjectValidator<FabricProfile>.IsNull(FabricProfile))
        {
            NotificationService.Error("error.download", nameof(FabricProfile));
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!_loaded)
            return false;

        FabricLoader fabricLoader = FabricVersionHelper.GetLoaderVersion(_launcherVersion, FabricVersionManifest);
        if (ObjectValidator<FabricLoader>.IsNull(fabricLoader))
        {
            NotificationService.Error("error.parse", _launcherVersion?.FabricLoaderVersion, nameof(FabricLoader));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!_loaded)
            return false;

        if (
            !await FabricLoaderDownloader.Download(
                _launcherPath,
                _launcherVersion,
                _launcherInstance,
                FabricProfile,
                _fabricUrls
            )
        )
        {
            NotificationService.Error("error.download", nameof(FabricLoaderDownloader));
            return false;
        }

        return true;
    }
}
