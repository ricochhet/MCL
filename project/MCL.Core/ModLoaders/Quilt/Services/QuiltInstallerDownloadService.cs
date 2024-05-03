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
using MCL.Core.ModLoaders.Interfaces.Services;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltInstallerDownloadService : IModLoaderInstallerDownloadService
{
    public QuiltVersionManifest? QuiltVersionManifest { get; private set; }
    public QuiltInstaller? QuiltInstaller { get; private set; }
    private readonly LauncherPath? _launcherPath;
    private readonly LauncherVersion? _launcherVersion;
    private readonly QuiltUrls? _quiltUrls;

    private QuiltInstallerDownloadService() { }

    public QuiltInstallerDownloadService(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        QuiltUrls? quiltUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _quiltUrls = quiltUrls;
    }

    /// <inheritdoc />
    public async Task<bool> Download(bool loadLocalVersionManifest = false)
    {
        if (!loadLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
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
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest))
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

        return ObjectValidator<QuiltVersionManifest>.IsNotNull(QuiltVersionManifest, NativeLogLevel.Debug);
    }

    /// <inheritdoc />
    public bool LoadVersion()
    {
        QuiltInstaller = QuiltVersionHelper.GetInstallerVersion(_launcherVersion, QuiltVersionManifest);
        if (ObjectValidator<QuiltInstaller>.IsNull(QuiltInstaller))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.QuiltInstallerVersion ?? ValidationShims.StringEmpty(),
                nameof(QuiltInstaller)
            );
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DownloadJar()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await QuiltInstallerDownloader.Download(_launcherPath, _launcherVersion, QuiltInstaller))
            {
                NotificationProvider.Error("error.download", nameof(QuiltInstallerDownloader));
                return false;
            }

            return true;
        });
    }
}
