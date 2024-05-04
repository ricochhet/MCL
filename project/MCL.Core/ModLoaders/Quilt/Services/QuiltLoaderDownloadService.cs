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
using MCL.Core.ModLoaders.Interfaces.Services;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;
using MiniCommon.Decorators;
using MiniCommon.IO;
using MiniCommon.Logger.Enums;
using MiniCommon.Providers;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltLoaderDownloadService : IModLoaderLoaderDownloadService
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
        if (ClassValidator.IsNull(launcherInstance))
            return;

        _launcherInstance = launcherInstance;
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _quiltUrls = quiltUrls;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public bool LoadVersionManifest()
    {
        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(
            QuiltPathResolver.VersionManifestPath(_launcherPath),
            QuiltVersionManifestContext.Default
        );
        if (ClassValidator.IsNull(QuiltVersionManifest))
        {
            NotificationProvider.Error("error.readfile", nameof(QuiltVersionManifest));
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool LoadVersionManifestWithoutLogging()
    {
        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(
            QuiltPathResolver.VersionManifestPath(_launcherPath),
            QuiltVersionManifestContext.Default
        );

        return ClassValidator.IsNotNull(QuiltVersionManifest, NativeLogLevel.Debug);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public bool LoadProfile()
    {
        if (StringValidator.IsNullOrWhiteSpace([_launcherVersion?.MVersion, _launcherVersion?.QuiltLoaderVersion]))
        {
            return false;
        }

        QuiltProfile = Json.Load<QuiltProfile>(
            QuiltPathResolver.ProfilePath(_launcherPath, _launcherVersion),
            QuiltProfileContext.Default
        );
        if (ClassValidator.IsNull(QuiltProfile))
        {
            NotificationProvider.Error("error.download", nameof(QuiltProfile));
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool LoadLoaderVersion()
    {
        QuiltLoader? quiltLoader = QuiltVersionHelper.GetLoaderVersion(_launcherVersion, QuiltVersionManifest);
        if (ClassValidator.IsNull(quiltLoader))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.QuiltLoaderVersion ?? StringOperator.Empty(),
                nameof(QuiltLoader)
            );
            return false;
        }

        return true;
    }

    /// <inheritdoc />
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
