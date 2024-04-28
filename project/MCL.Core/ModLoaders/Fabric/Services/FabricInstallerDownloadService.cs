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
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricInstallerDownloadService : IJarDownloadService<FabricUrls>, IDownloadService
{
    public static FabricVersionManifest? FabricVersionManifest { get; private set; }
    public static FabricInstaller? FabricInstaller { get; private set; }
    private static LauncherPath? _launcherPath;
    private static LauncherVersion? _launcherVersion;
    private static FabricUrls? _fabricUrls;
    private static bool _initialized = false;

    /// <summary>
    /// Initialize the Fabric installer download service.
    /// </summary>
    public static void Init(LauncherPath? launcherPath, LauncherVersion? launcherVersion, FabricUrls? fabricUrls)
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _fabricUrls = fabricUrls;
        _initialized = true;
    }

    /// <summary>
    /// Download all parts of the Fabric installer.
    /// </summary>
    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!_initialized)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
            return false;

        return true;
    }

    /// <summary>
    /// Download the Fabric version manifest.
    /// </summary>
    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_initialized)
            return false;

        if (!await FabricVersionManifestDownloader.Download(_launcherPath, _fabricUrls))
        {
            NotificationService.Error("error.download", nameof(FabricVersionManifestDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Fabric version manifest from the download path.
    /// </summary>
    public static bool LoadVersionManifest()
    {
        if (!_initialized)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricVersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(FabricVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Fabric version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_initialized)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricVersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    /// <summary>
    /// Load the Fabric installer version specified by the FabricInstallerVersion from the FabricVersionManifest download path.
    /// </summary>
    public static bool LoadVersion()
    {
        if (!_initialized)
            return false;

        FabricInstaller = FabricVersionHelper.GetInstallerVersion(_launcherVersion, FabricVersionManifest);
        if (ObjectValidator<FabricInstaller>.IsNull(FabricInstaller))
        {
            NotificationService.Error(
                "error.parse",
                _launcherVersion?.FabricInstallerVersion ?? ValidationShims.StringEmpty(),
                nameof(FabricInstaller)
            );
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the Fabric installer jar.
    /// </summary>
    public static async Task<bool> DownloadJar()
    {
        if (!_initialized)
            return false;

        if (!await FabricInstallerDownloader.Download(_launcherPath, _launcherVersion, FabricInstaller))
        {
            NotificationService.Error("error.download", nameof(FabricInstallerDownloader));
            return false;
        }

        return true;
    }
}
