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
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;
using MCL.Core.ModLoaders.Interfaces.Services;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricLoaderDownloadService : IModLoaderLoaderDownloadService
{
    public FabricVersionManifest? FabricVersionManifest { get; private set; }
    public FabricProfile? FabricProfile { get; private set; }
    private readonly LauncherPath? _launcherPath;
    private readonly LauncherVersion? _launcherVersion;
    private readonly LauncherInstance? _launcherInstance;
    private readonly FabricUrls? _fabricUrls;

    private FabricLoaderDownloadService() { }

    public FabricLoaderDownloadService(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        FabricUrls? fabricUrls
    )
    {
        if (ClassValidator.IsNull(launcherInstance))
            return;

        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherInstance = launcherInstance;
        _fabricUrls = fabricUrls;
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
        if (ClassValidator.IsNull(FabricVersionManifest))
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

        return ClassValidator.IsNotNull(FabricVersionManifest, NativeLogLevel.Debug);
    }

    /// <inheritdoc />
    public async Task<bool> DownloadProfile()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await FabricProfileDownloader.Download(_launcherPath, _launcherVersion, _fabricUrls))
            {
                NotificationProvider.Error("error.download", nameof(FabricProfileDownloader));
                return false;
            }

            return true;
        });
    }

    /// <inheritdoc />
    public bool LoadProfile()
    {
        if (StringValidator.IsNullOrWhiteSpace([_launcherVersion?.MVersion, _launcherVersion?.FabricLoaderVersion]))
        {
            return false;
        }

        FabricProfile = Json.Load<FabricProfile>(
            FabricPathResolver.ProfilePath(_launcherPath, _launcherVersion),
            FabricProfileContext.Default
        );
        if (ClassValidator.IsNull(FabricProfile))
        {
            NotificationProvider.Error("error.download", nameof(FabricProfile));
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool LoadLoaderVersion()
    {
        FabricLoader? fabricLoader = FabricVersionHelper.GetLoaderVersion(_launcherVersion, FabricVersionManifest);
        if (ClassValidator.IsNull(fabricLoader))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.FabricLoaderVersion ?? StringOperator.Empty(),
                nameof(FabricLoader)
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
                !await FabricLoaderDownloader.Download(
                    _launcherPath,
                    _launcherVersion,
                    _launcherInstance,
                    FabricProfile,
                    _fabricUrls
                )
            )
            {
                NotificationProvider.Error("error.download", nameof(FabricLoaderDownloader));
                return false;
            }

            return true;
        });
    }
}
