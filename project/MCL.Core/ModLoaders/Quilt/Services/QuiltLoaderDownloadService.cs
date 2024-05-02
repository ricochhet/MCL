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
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Decorators;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltLoaderDownloadService
{
    public QuiltVersionManifest? QuiltVersionManifest { get; private set; }
    public QuiltProfile? QuiltProfile { get; private set; }
    private readonly LauncherPath? _launcherPath;
    private readonly LauncherVersion? _launcherVersion;
    private readonly LauncherInstance? _launcherInstance;
    private readonly QuiltUrls? _quiltUrls;

    private QuiltLoaderDownloadService() { }

    public QuiltLoaderDownloadService(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        QuiltUrls? quiltUrls
    )
    {
        if (ObjectValidator<LauncherInstance>.IsNull(launcherInstance))
            return;

        _launcherInstance = launcherInstance;
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _quiltUrls = quiltUrls;
    }

    /// <summary>
    /// Download all parts of the Quilt loader.
    /// </summary>
    public async Task<bool> Download(bool loadLocalVersionManifest = false, bool loadLocalVersionDetails = false)
    {
        if (!loadLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!loadLocalVersionDetails && !await DownloadProfile())
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
    public async Task<bool> DownloadVersionManifest()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await QuiltVersionManifestDownloader.Download(_launcherPath, _quiltUrls))
            {
                NotificationProvider.Error("error.download", nameof(QuiltVersionManifestDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the Quilt version manifest from the download path.
    /// </summary>
    public bool LoadVersionManifest()
    {
        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(
            QuiltPathResolver.VersionManifestPath(_launcherPath),
            QuiltVersionManifestContext.Default
        );
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest))
        {
            NotificationProvider.Error("error.readfile", nameof(QuiltVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public bool LoadVersionManifestWithoutLogging()
    {
        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(
            QuiltPathResolver.VersionManifestPath(_launcherPath),
            QuiltVersionManifestContext.Default
        );
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    /// <summary>
    /// Exclusively download the Quilt profile.
    /// </summary>
    public async Task<bool> DownloadProfile()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await QuiltProfileDownloader.Download(_launcherPath, _launcherVersion, _quiltUrls))
            {
                NotificationProvider.Error("error.download", nameof(QuiltProfileDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the Quilt profile from the download path.
    /// </summary>
    public bool LoadProfile()
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [_launcherVersion?.MVersion, _launcherVersion?.QuiltLoaderVersion]
            )
        )
            return false;

        QuiltProfile = Json.Load<QuiltProfile>(
            QuiltPathResolver.ProfilePath(_launcherPath, _launcherVersion),
            QuiltProfileContext.Default
        );
        if (ObjectValidator<QuiltProfile>.IsNull(QuiltProfile))
        {
            NotificationProvider.Error("error.download", nameof(QuiltProfile));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Quilt loader version specified by the QuiltLoaderVersion from the QuiltVersionManifest download path.
    /// </summary>
    public bool LoadLoaderVersion()
    {
        QuiltLoader? quiltLoader = QuiltVersionHelper.GetLoaderVersion(_launcherVersion, QuiltVersionManifest);
        if (ObjectValidator<QuiltLoader>.IsNull(quiltLoader))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.QuiltLoaderVersion ?? ValidationShims.StringEmpty(),
                nameof(QuiltLoader)
            );
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the Quilt loader jar.
    /// </summary>
    public async Task<bool> DownloadLoader()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
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
                NotificationProvider.Error("error.download", nameof(QuiltLoaderDownloader));
                return false;
            }

            return true;
        });
    }
}
