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
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltLoaderDownloadService : ILoaderDownloadService<QuiltUrls>, IDownloadService
{
    public static QuiltVersionManifest QuiltVersionManifest { get; private set; }
    public static QuiltProfile QuiltProfile { get; private set; }
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static LauncherInstance _launcherInstance;
    private static QuiltUrls _quiltUrls;
    private static bool _loaded = false;

    /// <summary>
    /// Initialize the Quilt loader download service.
    /// </summary>
    public static void Init(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        QuiltUrls quiltUrls
    )
    {
        if (ObjectValidator<LauncherInstance>.IsNull(launcherInstance))
            return;

        _launcherInstance = launcherInstance;
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _quiltUrls = quiltUrls;
        _loaded = true;
    }

    /// <summary>
    /// Download all parts of the Quilt loader.
    /// </summary>
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

    /// <summary>
    /// Download the Quilt version manifest.
    /// </summary>
    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await QuiltVersionManifestDownloader.Download(_launcherPath, _quiltUrls))
        {
            NotificationService.Error("error.download", nameof(QuiltVersionManifestDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt version manifest from the download path.
    /// </summary>
    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(QuiltVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    /// <summary>
    /// Exclusively download the Quilt profile.
    /// </summary>
    public static async Task<bool> DownloadProfile()
    {
        if (!_loaded)
            return false;

        if (!await QuiltProfileDownloader.Download(_launcherPath, _launcherVersion, _quiltUrls))
        {
            NotificationService.Error("error.download", nameof(QuiltProfileDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt profile from the download path.
    /// </summary>
    public static bool LoadProfile()
    {
        if (!_loaded)
            return false;

        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [_launcherVersion?.MVersion, _launcherVersion?.QuiltLoaderVersion]
            )
        )
            return false;

        QuiltProfile = Json.Load<QuiltProfile>(QuiltPathResolver.ProfilePath(_launcherPath, _launcherVersion));
        if (ObjectValidator<QuiltProfile>.IsNull(QuiltProfile))
        {
            NotificationService.Error("error.download", nameof(QuiltProfile));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt loader version specified by the QuiltLoaderVersion from the QuiltVersionManifest download path.
    /// </summary>
    public static bool LoadLoaderVersion()
    {
        if (!_loaded)
            return false;

        QuiltLoader quiltLoader = QuiltVersionHelper.GetLoaderVersion(_launcherVersion, QuiltVersionManifest);
        if (ObjectValidator<QuiltLoader>.IsNull(quiltLoader))
        {
            NotificationService.Error("error.parse", _launcherVersion?.QuiltLoaderVersion, nameof(QuiltLoader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the Quilt loader.
    /// </summary>
    public static async Task<bool> DownloadLoader()
    {
        if (!_loaded)
            return false;

        if (
            !await QuiltLoaderDownloader.Download(
                _launcherPath,
                _launcherVersion,
                _launcherInstance,
                QuiltProfile,
                _quiltUrls
            )
        )
        {
            NotificationService.Error("error.download", nameof(QuiltLoaderDownloader));
            return false;
        }

        return true;
    }
}
