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
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Web;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Minecraft.Services;

public class MDownloadService : IDownloadService
{
    public static MVersionManifest? VersionManifest { get; private set; }
    public static MVersionDetails? VersionDetails { get; private set; }
    public static MVersion? Version { get; private set; }
    private static MAssetsData? _assets;
    private static LauncherPath? _launcherPath;
    private static LauncherVersion? _launcherVersion;
    private static LauncherSettings? _launcherSettings;
    private static LauncherInstance? _launcherInstance;
    private static MUrls? _mUrls;
    private static bool _initialized = false;

    /// <summary>
    /// Initialize the game download service.
    /// </summary>
    public static void Init(
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
        _initialized = true;
    }

#pragma warning disable IDE0079
#pragma warning disable S3776
    /// <summary>
    /// Download all parts of the game.
    /// </summary>
    public static async Task<bool> Download(bool useLocalVersionManifest = false)
#pragma warning restore IDE0079, S3776
    {
        if (!_initialized)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadVersionDetails())
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

        if (!await DownloadAssetIndex())
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
    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_initialized)
            return false;

        if (!await VersionManifestDownloader.Download(_launcherPath, _mUrls))
        {
            NotificationService.Error("error.download", nameof(MVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the game version manifest from the download path.
    /// </summary>
    public static bool LoadVersionManifest()
    {
        if (!_initialized)
            return false;

        VersionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<MVersionManifest>.IsNull(VersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(MVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the game version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_initialized)
            return false;

        VersionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<MVersionManifest>.IsNull(VersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    /// <summary>
    /// Load the game version specified by the MVersion from the MVersionManifest download path.
    /// </summary>
    public static bool LoadVersion()
    {
        if (!_initialized)
            return false;

        Version = VersionHelper.GetVersion(_launcherVersion, VersionManifest);
        if (ObjectValidator<MVersion>.IsNull(Version))
        {
            NotificationService.Error(
                "error.parse",
                _launcherVersion?.MVersion ?? ValidationShims.StringEmpty(),
                nameof(MVersion)
            );
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game version details.
    /// </summary>
    public static async Task<bool> DownloadVersionDetails()
    {
        if (!_initialized)
            return false;

        if (!await VersionDetailsDownloader.Download(_launcherPath, Version))
        {
            NotificationService.Error("error.download", nameof(VersionDetailsDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the game version details from the download path.
    /// </summary>
    public static bool LoadVersionDetails()
    {
        if (!_initialized)
            return false;

        VersionDetails = Json.Load<MVersionDetails>(MPathResolver.VersionDetailsPath(_launcherPath, Version));
        if (ObjectValidator<MVersionDetails>.IsNull(VersionDetails))
        {
            NotificationService.Error("error.readfile", nameof(MVersionDetails));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game libraries.
    /// </summary>
    public static async Task<bool> DownloadLibraries()
    {
        if (!_initialized)
            return false;

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
            NotificationService.Error("error.download", nameof(LibraryDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game client.
    /// </summary>
    public static async Task<bool> DownloadClient()
    {
        if (!_initialized)
            return false;

        if (!await ClientDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(ClientDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game client mappings
    /// </summary>
    public static async Task<bool> DownloadClientMappings()
    {
        if (!_initialized)
            return false;

        if (!await ClientMappingsDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(ClientMappingsDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game server.
    /// </summary>
    public static async Task<bool> DownloadServer()
    {
        if (!_initialized)
            return false;

        if (!await ServerDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(ServerDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game server mappings.
    /// </summary>
    public static async Task<bool> DownloadServerMappings()
    {
        if (!_initialized)
            return false;

        if (!await ServerMappingsDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(ServerMappingsDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the game asset index.
    /// </summary>
    public static async Task<bool> DownloadAssetIndex()
    {
        if (!_initialized)
            return false;

        if (!await AssetIndexDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(AssetIndexDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the game asset index from the download path.
    /// </summary>
    public static bool LoadAssetIndex()
    {
        if (!_initialized)
            return false;

        _assets = Json.Load<MAssetsData>(MPathResolver.ClientAssetIndexPath(_launcherPath, VersionDetails));
        if (ObjectValidator<MAssetsData>.IsNull(_assets))
        {
            NotificationService.Error("error.readfile", nameof(MAssetsData));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game resources.
    /// </summary>
    public static async Task<bool> DownloadResources()
    {
        if (!_initialized)
            return false;

        if (!await ResourceDownloader.Download(_launcherPath, _mUrls, _assets))
        {
            NotificationService.Error("error.download", nameof(ResourceDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Exclusively download the game logging configuration.
    /// </summary>
    public static async Task<bool> DownloadLogging()
    {
        if (!_initialized)
            return false;

        if (!await LoggingDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Error("error.download", nameof(LoggingDownloader));
            return false;
        }

        return true;
    }
}
