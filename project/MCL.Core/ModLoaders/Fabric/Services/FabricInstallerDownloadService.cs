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
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;
using MCL.Core.ModLoaders.Interfaces.Services;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricInstallerDownloadService : IModLoaderInstallerDownloadService
{
    public FabricVersionManifest? FabricVersionManifest { get; private set; }
    public FabricInstaller? FabricInstaller { get; private set; }
    private readonly LauncherPath? _launcherPath;
    private readonly LauncherVersion? _launcherVersion;
    private readonly FabricUrls? _fabricUrls;

    private FabricInstallerDownloadService() { }

    public FabricInstallerDownloadService(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        FabricUrls? fabricUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _fabricUrls = fabricUrls;
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
            if (!await FabricVersionManifestDownloader.Download(_launcherPath, _fabricUrls))
            {
                NotificationProvider.Error("error.download", nameof(FabricVersionManifestDownloader));
                return false;
            }

            return true;
        });
    }

    /// <inheritdoc />
    public bool LoadVersionManifest()
    {
        FabricVersionManifest = Json.Load<FabricVersionManifest>(
            FabricPathResolver.VersionManifestPath(_launcherPath),
            FabricVersionManifestContext.Default
        );
        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricVersionManifest))
        {
            NotificationProvider.Error("error.readfile", nameof(FabricVersionManifest));
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool LoadVersionManifestWithoutLogging()
    {
        FabricVersionManifest = Json.Load<FabricVersionManifest>(
            FabricPathResolver.VersionManifestPath(_launcherPath),
            FabricVersionManifestContext.Default
        );

        return ObjectValidator<FabricVersionManifest>.IsNotNull(FabricVersionManifest, NativeLogLevel.Debug);
    }

    /// <inheritdoc />
    public bool LoadVersion()
    {
        FabricInstaller = FabricVersionHelper.GetInstallerVersion(_launcherVersion, FabricVersionManifest);
        if (ObjectValidator<FabricInstaller>.IsNull(FabricInstaller))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.FabricInstallerVersion ?? ValidationShims.StringEmpty(),
                nameof(FabricInstaller)
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
            if (!await FabricInstallerDownloader.Download(_launcherPath, _launcherVersion, FabricInstaller))
            {
                NotificationProvider.Error("error.download", nameof(FabricInstallerDownloader));
                return false;
            }

            return true;
        });
    }
}
