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
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Java.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MiniCommon.Decorators;
using MiniCommon.IO;
using MiniCommon.Logger.Enums;
using MiniCommon.Providers;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Java.Services;

public class JavaDownloadService
{
    private JavaVersionManifest? _javaVersionManifest;
    private JavaVersionDetails? _javaVersionDetails;
    private readonly LauncherPath? _launcherPath;
    private readonly MUrls? _mUrls;
    private readonly JavaRuntimeType? _javaRuntimeType;
    private readonly JavaRuntimePlatform? _javaRuntimePlatform;

    private JavaDownloadService() { }

    public JavaDownloadService(
        LauncherPath? launcherPath,
        MUrls? mUrls,
        JavaRuntimeType? javaRuntimeType,
        JavaRuntimePlatform? javaRuntimePlatform
    )
    {
        _launcherPath = launcherPath;
        _mUrls = mUrls;
        _javaRuntimeType = javaRuntimeType;
        _javaRuntimePlatform = javaRuntimePlatform;
    }

    /// <summary>
    /// Download all parts of the Java runtime environment.
    /// </summary>
    public async Task<bool> Download(bool loadLocalVersionManifest = false, bool loadLocalVersionDetails = false)
    {
        if (!loadLocalVersionManifest && !await DownloadJavaVersionManifest())
            return false;

        if (!LoadJavaVersionManifest())
            return false;

        if (!loadLocalVersionDetails && !await DownloadJavaVersionDetails())
            return false;

        if (!LoadJavaVersionDetails())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    /// <summary>
    /// Exclusively download the Java version manifest.
    /// </summary>
    public async Task<bool> DownloadJavaVersionManifest()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await JavaVersionManifestDownloader.Download(_launcherPath, _mUrls))
            {
                NotificationProvider.Error("error.download", nameof(JavaVersionManifestDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the Java version manifest from the download path.
    /// </summary>
    public bool LoadJavaVersionManifest()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.JavaVersionManifestPath(_launcherPath),
            JavaVersionManifestContext.Default
        );
        if (ClassValidator.IsNull(_javaVersionManifest))
        {
            NotificationProvider.Error("error.readfile", nameof(_javaVersionManifest));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Load the Java version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public bool LoadJavaVersionManifestWithoutLogging()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.JavaVersionManifestPath(_launcherPath),
            JavaVersionManifestContext.Default
        );

        return ClassValidator.IsNotNull(_javaVersionManifest, NativeLogLevel.Debug);
    }

    /// <summary>
    /// Exclusively download the Java version details.
    /// </summary>
    public async Task<bool> DownloadJavaVersionDetails()
    {
        return await TimingDecorator.TimeAsync(async () =>
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
                NotificationProvider.Error("error.download", nameof(JavaVersionDetailsDownloader));
                return false;
            }

            return true;
        });
    }

    /// <summary>
    /// Load the Java version details from the download path.
    /// </summary>
    public bool LoadJavaVersionDetails()
    {
        _javaVersionDetails = Json.Load<JavaVersionDetails>(
            JavaPathResolver.JavaVersionDetailsPath(_launcherPath, JavaRuntimeTypeResolver.ToString(_javaRuntimeType)),
            JavaVersionDetailsContext.Default
        );
        if (ClassValidator.IsNull(_javaVersionDetails))
        {
            NotificationProvider.Error("error.readfile", nameof(_javaVersionDetails));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Download the Java runtime environment.
    /// </summary>
    public async Task<bool> DownloadJavaRuntime()
    {
        return await TimingDecorator.TimeAsync(async () =>
        {
            if (!await JavaRuntimeDownloader.Download(_launcherPath, _javaRuntimeType, _javaVersionDetails))
            {
                NotificationProvider.Error("error.download", nameof(JavaRuntimeDownloader));
                return false;
            }

            return true;
        });
    }
}
