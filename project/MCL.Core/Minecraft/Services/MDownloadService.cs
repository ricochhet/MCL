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
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Web;
using MCL.Core.MiniCommon.Decorators;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.MiniCommon.Validation.Validators;

namespace MCL.Core.Minecraft.Services;

public class MDownloadService
{
    public MVersionManifest? VersionManifest { get; private set; }
    public MVersionDetails? VersionDetails { get; private set; }
    public MVersion? Version { get; private set; }
    private MAssetsData? _assets;
    private readonly LauncherPath? _launcherPath;
    private readonly LauncherVersion? _launcherVersion;
    private readonly LauncherSettings? _launcherSettings;
    private readonly LauncherInstance? _launcherInstance;
    private readonly MUrls? _mUrls;

    private MDownloadService() { }

    public MDownloadService(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherSettings? launcherSettings,
        LauncherInstance? launcherInstance,
        MUrls? mUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherInstance = launcherInstance;
        _launcherSettings = launcherSettings;
        _mUrls = mUrls;
    }

#pragma warning disable S3776
    /// <summary>
    /// Download all parts of the game.
    /// </summary>
    public async Task<bool> Download(
        bool loadLocalVersionManifest = false,
        bool loadLocalVersionDetails = false,
        bool loadLocalAssetIndex = false
    )
#pragma warning restore S3776
    {
        if (!loadLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!loadLocalVersionDetails && !await DownloadVersionDetails())
            return false;

        if (!LoadVersionDetails())
            return false;

        if (!await DownloadLibraries())
            return false;

        if (!await DownloadClient())
            return false;

        if (!await DownloadClientMappings())
            return false;

        if (!await DownloadServer())
            return false;

        if (!await DownloadServerMappings())
            return false;

        if (!loadLocalAssetIndex && !await DownloadAssetIndex())
            return false;

        if (!LoadAssetIndex())
            return false;

        if (!await DownloadResources())
            return false;

        if (!await DownloadLogging())
            return false;

        return true;
    }

    /// <summary>
    /// Exclusively download the game version manifest.
    /// </summary>
    public async Task<bool> DownloadVersionManifest()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await VersionManifestDownloader.Download(_launcherPath, _mUrls))
            {
                NotificationProvider.Error("error.download", nameof(MVersionManifest));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the game version manifest from the download path.
    /// </summary>
    public bool LoadVersionManifest()
    {
        VersionManifest = Json.Load<MVersionManifest>(
            MPathResolver.VersionManifestPath(_launcherPath),
            MVersionManifestContext.Default
        );
        if (ClassValidator.IsNull(VersionManifest))
        {
            NotificationProvider.Error("error.readfile", nameof(MVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the game version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public bool LoadVersionManifestWithoutLogging()
    {
        VersionManifest = Json.Load<MVersionManifest>(
            MPathResolver.VersionManifestPath(_launcherPath),
            MVersionManifestContext.Default
        );

        return ClassValidator.IsNotNull(VersionManifest, NativeLogLevel.Debug);
    }

    /// <summary>
    /// Load the game version specified by the MVersion from the MVersionManifest download path.
    /// </summary>
    public bool LoadVersion()
    {
        Version = VersionHelper.GetVersion(_launcherVersion, VersionManifest);
        if (ClassValidator.IsNull(Version))
        {
            NotificationProvider.Error(
                "error.parse",
                _launcherVersion?.MVersion ?? StringOperator.Empty(),
                nameof(MVersion)
            );
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game version details.
    /// </summary>
    public async Task<bool> DownloadVersionDetails()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await VersionDetailsDownloader.Download(_launcherPath, Version))
            {
                NotificationProvider.Error("error.download", nameof(VersionDetailsDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the game version details from the download path.
    /// </summary>
    public bool LoadVersionDetails()
    {
        VersionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(_launcherPath, Version),
            MVersionDetailsContext.Default
        );
        if (ClassValidator.IsNull(VersionDetails))
        {
            NotificationProvider.Error("error.readfile", nameof(MVersionDetails));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game libraries.
    /// </summary>
    public async Task<bool> DownloadLibraries()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (
                !await LibraryDownloader.Download(
                    _launcherPath,
                    _launcherVersion,
                    _launcherInstance,
                    _launcherSettings,
                    VersionDetails
                )
            )
            {
                NotificationProvider.Error("error.download", nameof(LibraryDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Exclusively download the game client.
    /// </summary>
    public async Task<bool> DownloadClient()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await ClientDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(ClientDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Exclusively download the game client mappings
    /// </summary>
    public async Task<bool> DownloadClientMappings()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await ClientMappingsDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(ClientMappingsDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Exclusively download the game server.
    /// </summary>
    public async Task<bool> DownloadServer()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await ServerDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(ServerDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Exclusively download the game server mappings.
    /// </summary>
    public async Task<bool> DownloadServerMappings()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await ServerMappingsDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(ServerMappingsDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Download the game asset index.
    /// </summary>
    public async Task<bool> DownloadAssetIndex()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await AssetIndexDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(AssetIndexDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the game asset index from the download path.
    /// </summary>
    public bool LoadAssetIndex()
    {
        _assets = Json.Load<MAssetsData>(
            MPathResolver.ClientAssetIndexPath(_launcherPath, VersionDetails),
            MAssetsDataContext.Default
        );
        if (ClassValidator.IsNull(_assets))
        {
            NotificationProvider.Error("error.readfile", nameof(MAssetsData));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game resources.
    /// </summary>
    public async Task<bool> DownloadResources()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await ResourceDownloader.Download(_launcherPath, _mUrls, _assets))
            {
                NotificationProvider.Error("error.download", nameof(ResourceDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Exclusively download the game logging configuration.
    /// </summary>
    public async Task<bool> DownloadLogging()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await LoggingDownloader.Download(_launcherPath, VersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(LoggingDownloader));
                return false;
            }

            return true;
        });
    }
}
