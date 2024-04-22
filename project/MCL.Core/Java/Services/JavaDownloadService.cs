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
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Java.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.Java.Services;

public class JavaDownloadService : IDownloadService
{
    private static JavaVersionManifest _javaVersionManifest;
    private static JavaVersionDetails _javaVersionDetails;
    private static LauncherPath _launcherPath;
    private static MUrls _mUrls;
    private static JavaRuntimeType _javaRuntimeType;
    private static JavaRuntimePlatform _javaRuntimePlatform;

    public static void Init(
        LauncherPath launcherPath,
        MUrls mUrls,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        _launcherPath = launcherPath;
        _mUrls = mUrls;
        _javaRuntimeType = javaRuntimeType;
        _javaRuntimePlatform = javaRuntimePlatform;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!useLocalVersionManifest && !await DownloadJavaVersionManifest())
            return false;

        if (!LoadJavaVersionManifest())
            return false;

        if (!await DownloadJavaVersionDetails())
            return false;

        if (!LoadJavaVersionDetails())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaVersionManifest()
    {
        if (!await JavaVersionManifestDownloader.Download(_launcherPath, _mUrls))
        {
            NotificationService.Error("error.download", nameof(JavaVersionManifestDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionManifest()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.DownloadedJavaVersionManifestPath(_launcherPath)
        );
        if (ObjectValidator<JavaVersionManifest>.IsNull(_javaVersionManifest))
        {
            NotificationService.Error("error.readfile", nameof(_javaVersionManifest));
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionManifestWithoutLogging()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.DownloadedJavaVersionManifestPath(_launcherPath)
        );
        if (ObjectValidator<JavaVersionManifest>.IsNull(_javaVersionManifest, NativeLogLevel.Debug))
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaVersionDetails()
    {
        if (
            !await JavaVersionDetailsDownloader.Download(
                _launcherPath,
                _javaRuntimePlatform,
                _javaRuntimeType,
                _javaVersionManifest
            )
        )
        {
            NotificationService.Error("error.download", nameof(JavaVersionDetailsDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionDetails()
    {
        _javaVersionDetails = Json.Load<JavaVersionDetails>(
            JavaPathResolver.DownloadedJavaVersionDetailsPath(
                _launcherPath,
                JavaRuntimeTypeResolver.ToString(_javaRuntimeType)
            )
        );
        if (ObjectValidator<JavaVersionDetails>.IsNull(_javaVersionDetails))
        {
            NotificationService.Error("error.readfile", nameof(_javaVersionDetails));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(_launcherPath, _javaRuntimeType, _javaVersionDetails))
        {
            NotificationService.Error("error.download", nameof(JavaRuntimeDownloader));
            return false;
        }

        return true;
    }
}
