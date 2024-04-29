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
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MCL.Core.Servers.Paper.Web;

namespace MCL.Core.Servers.Paper.Services;

public static class PaperServerDownloadService
{
    public static PaperVersionManifest? PaperVersionManifest { get; private set; }
    public static PaperBuild? PaperBuild { get; private set; }
    private static LauncherPath? _launcherPath;
    private static LauncherVersion? _launcherVersion;
    private static LauncherInstance? _launcherInstance;
    private static PaperUrls? _paperUrls;
    private static bool _initialized = false;

    /// <summary>
    /// Initialize the Paper server download service.
    /// </summary>
    public static void Init(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        PaperUrls? paperUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherInstance = launcherInstance;
        _paperUrls = paperUrls;
        _initialized = true;
    }

    /// <summary>
    /// Download all parts of the Paper server.
    /// </summary>
    public static async Task<bool> Download(bool loadLocalVersionManifest = false)
    {
        if (!_initialized)
            return false;

        if (!loadLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
            return false;

        if (
            ObjectValidator<string>.IsNullOrEmpty(_launcherInstance?.PaperServerVersions)
            || ObjectValidator<LauncherVersion>.IsNull(_launcherVersion)
        )
            return false;

        foreach (string version in _launcherInstance!.PaperServerVersions!)
        {
            if (version == _launcherVersion!.PaperServerVersion)
                _launcherInstance!.PaperServerVersions.Remove(version);
        }

        _launcherInstance!.PaperServerVersions.Add(_launcherVersion!.PaperServerVersion);
        SettingsService.Load()?.Save(_launcherInstance);

        return true;
    }

    /// <summary>
    /// Download the Paper version manifest.
    /// </summary>
    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_initialized)
            return false;

        if (!await PaperVersionManifestDownloader.Download(_launcherPath, _launcherVersion, _paperUrls))
        {
            NotificationService.Error("error.download", nameof(PaperVersionManifestDownloader));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Paper version manifest from the download path.
    /// </summary>
    public static bool LoadVersionManifest()
    {
        if (!_initialized)
            return false;

        if (ObjectValidator<string>.IsNullOrWhiteSpace([_launcherVersion?.MVersion]))
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.VersionManifestPath(_launcherPath, _launcherVersion)
        );
        if (ObjectValidator<PaperVersionManifest>.IsNull(PaperVersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(PaperVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Paper version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_initialized)
            return false;

        if (ObjectValidator<string>.IsNullOrWhiteSpace([_launcherVersion?.MVersion], NativeLogLevel.Debug))
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.VersionManifestPath(_launcherPath, _launcherVersion)
        );
        if (ObjectValidator<PaperVersionManifest>.IsNull(PaperVersionManifest))
            return false;

        return true;
    }

    /// <summary>
    /// Load the Paper server version specified by the PaperServerVersion from the PaperVersionManifest download path.
    /// </summary>
    public static bool LoadVersion()
    {
        if (!_initialized)
            return false;

        PaperBuild = PaperVersionHelper.GetVersion(_launcherVersion, PaperVersionManifest);
        if (ObjectValidator<PaperBuild>.IsNull(PaperBuild))
        {
            NotificationService.Error(
                "error.parse",
                _launcherVersion?.PaperServerVersion ?? ValidationShims.StringEmpty(),
                nameof(PaperBuild)
            );
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the Paper server.
    /// </summary>
    public static async Task<bool> DownloadJar()
    {
        if (!_initialized)
            return false;

        if (!await PaperServerDownloader.Download(_launcherPath, _launcherVersion, PaperBuild, _paperUrls))
        {
            NotificationService.Error("error.download", nameof(PaperServerDownloader));
            return false;
        }

        return true;
    }
}
