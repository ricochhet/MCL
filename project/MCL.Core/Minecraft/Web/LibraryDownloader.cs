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

using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Enums;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class LibraryDownloader
{
    private static LauncherLoader _loader;

    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        LauncherSettings launcherSettings,
        MVersionDetails versionDetails
    )
    {
        if (ObjectValidator<List<MLibrary>>.IsNullOrEmpty(versionDetails?.Libraries))
            return false;

        string libPath = VFS.Combine(launcherPath.Path, "libraries");
        _loader = new() { Version = launcherVersion.Version };

        foreach (MLibrary lib in versionDetails.Libraries)
        {
            if (ObjectValidator<MLibraryDownloads>.IsNull(lib?.Downloads))
                return false;

            if (SkipLibrary(lib, launcherSettings))
                continue;

            if (!await DownloadNatives(launcherPath, lib, launcherSettings))
                return false;

            if (
                ObjectValidator<string>.IsNullOrWhiteSpace(
                    [lib.Downloads?.Artifact?.Path, lib.Downloads?.Artifact?.URL, lib.Downloads?.Artifact?.SHA1]
                )
            )
                return false;

            string filepath = VFS.Combine(libPath, lib.Downloads.Artifact.Path);
            if (!await Request.DownloadSHA1(lib.Downloads.Artifact.URL, filepath, lib.Downloads.Artifact.SHA1))
                return false;
            _loader.Libraries.Add(filepath);
        }

        foreach (LauncherLoader existingLoader in launcherInstance.Versions)
        {
            if (existingLoader.Version == _loader.Version)
                launcherInstance.Versions.Remove(existingLoader);
        }

        launcherInstance.Versions.Add(_loader);
        SettingsService.Load().Save(launcherInstance);
        return true;
    }

    public static bool SkipLibrary(MLibrary lib, LauncherSettings launcherSettings)
    {
        if (ObjectValidator<List<MLibraryRule>>.IsNullOrEmpty(lib.Rules, NativeLogLevel.Debug))
            return false;

        bool allowLibrary = false;
        foreach (MLibraryRule rule in lib.Rules)
        {
            string action = rule.Action;
            string os = rule.Os?.Name;

            if (ObjectValidator<string>.IsNullOrWhiteSpace([os]))
            {
                allowLibrary = action == RuleTypeResolver.ToString(RuleType.ALLOW);
                continue;
            }

            if (os == JavaRuntimePlatformResolver.ToPlatformString(launcherSettings.JavaRuntimePlatform))
            {
                allowLibrary = action == RuleTypeResolver.ToString(RuleType.ALLOW);
            }
        }

        return !allowLibrary;
    }

    public static async Task<bool> DownloadNatives(
        LauncherPath launcherPath,
        MLibrary lib,
        LauncherSettings launcherSettings
    )
    {
        if (ObjectValidator<MClassifiers>.IsNull(lib.Downloads.Classifiers, NativeLogLevel.Debug))
            return true;

        string classifierFilePath = string.Empty;
        string classifierUrl = string.Empty;
        string classifierSha1 = string.Empty;
        string libraryPath = MPathResolver.LibraryPath(launcherPath);

        switch (launcherSettings.JavaRuntimePlatform)
        {
            case JavaRuntimePlatform.WINDOWSX64
            or JavaRuntimePlatform.WINDOWSX86
            or JavaRuntimePlatform.WINDOWSARM64:
                if (
                    ObjectValidator<string>.IsNullOrWhiteSpace(
                        [
                            lib.Downloads?.Classifiers?.NativesWindows?.URL,
                            lib.Downloads?.Classifiers?.NativesWindows?.SHA1,
                            lib.Downloads?.Classifiers?.NativesWindows?.Path
                        ]
                    )
                )
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesWindows.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesWindows.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesWindows.SHA1;
                break;
            case JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386:
                if (
                    ObjectValidator<string>.IsNullOrWhiteSpace(
                        [
                            lib.Downloads?.Classifiers?.NativesLinux?.URL,
                            lib.Downloads?.Classifiers?.NativesLinux?.SHA1,
                            lib.Downloads?.Classifiers?.NativesLinux?.Path
                        ]
                    )
                )
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesLinux.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesLinux.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesLinux.SHA1;
                break;
            case JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64:
                if (
                    ObjectValidator<string>.IsNullOrWhiteSpace(
                        [
                            lib.Downloads?.Classifiers?.NativesMacos?.URL,
                            lib.Downloads?.Classifiers?.NativesMacos?.SHA1,
                            lib.Downloads?.Classifiers?.NativesMacos?.Path
                        ]
                    )
                )
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesMacos.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesMacos.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesMacos.SHA1;
                break;
        }

        if (!await Request.DownloadSHA1(classifierUrl, classifierFilePath, classifierSha1))
            return false;
        _loader.Libraries.Add(classifierFilePath);
        return true;
    }
}
