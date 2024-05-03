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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Web;

namespace MCL.Core.Java.Web;

public static class JavaVersionDetailsDownloader
{
    /// <summary>
    /// Download the Java version details specified by the JavaRuntimePlatform and JavaRuntimeType.
    /// </summary>
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        JavaRuntimePlatform? javaRuntimePlatform,
        JavaRuntimeType? javaRuntimeType,
        JavaVersionManifest? javaVersionManifest
    )
    {
        if (ObjectValidator<JavaVersionManifest>.IsNull(javaVersionManifest))
            return false;

        string? request = javaRuntimePlatform switch
        {
            JavaRuntimePlatform.GAMECORE => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.Gamecore),
            JavaRuntimePlatform.LINUX => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.Linux),
            JavaRuntimePlatform.LINUXI386 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.LinuxI386),
            JavaRuntimePlatform.MACOS => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.Macos),
            JavaRuntimePlatform.MACOSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.MacosArm64),
            JavaRuntimePlatform.WINDOWSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.WindowsArm64),
            JavaRuntimePlatform.WINDOWSX64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.WindowsX64),
            JavaRuntimePlatform.WINDOWSX86 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest!?.WindowsX86),
            _ => string.Empty,
        };
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request]))
            return false;

        string? javaRuntimeFiles = await Request.GetJsonAsync<JavaVersionDetails>(
            request!,
            JavaPathResolver.JavaVersionDetailsPath(launcherPath, JavaRuntimeTypeResolver.ToString(javaRuntimeType)),
            Encoding.UTF8,
            JavaVersionDetailsContext.Default
        );
        return ObjectValidator<string>.IsNotNullOrWhiteSpace([javaRuntimeFiles]);
    }

    public static string? GetJavaRuntimeUrl(JavaRuntimeType? javaRuntimeType, JavaRuntime? javaRuntime) =>
        javaRuntimeType switch
        {
            JavaRuntimeType.JAVA_RUNTIME_ALPHA
                => ObjectsValidate(javaRuntime, javaRuntime?.JavaRuntimeAlpha)
                    ? javaRuntime?.JavaRuntimeAlpha?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.JAVA_RUNTIME_BETA
                => ObjectsValidate(javaRuntime, javaRuntime?.JavaRuntimeBeta)
                    ? javaRuntime?.JavaRuntimeBeta?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.JAVA_RUNTIME_DELTA
                => ObjectsValidate(javaRuntime, javaRuntime?.JavaRuntimeDelta)
                    ? javaRuntime?.JavaRuntimeDelta?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA
                => ObjectsValidate(javaRuntime, javaRuntime?.JavaRuntimeGamma)
                    ? javaRuntime?.JavaRuntimeGamma?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA_SNAPSHOT
                => ObjectsValidate(javaRuntime, javaRuntime?.JavaRuntimeGammaSnapshot)
                    ? javaRuntime?.JavaRuntimeGammaSnapshot?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.JRE_LEGACY
                => ObjectsValidate(javaRuntime, javaRuntime?.JreLegacy)
                    ? javaRuntime?.JreLegacy?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            JavaRuntimeType.MINECRAFT_JAVA_EXE
                => ObjectsValidate(javaRuntime, javaRuntime?.MinecraftJavaExe)
                    ? javaRuntime?.MinecraftJavaExe?.FirstOrDefault()?.JavaRuntimeManifest.Url
                    : string.Empty,
            _ => string.Empty
        };

    private static bool ObjectsValidate(JavaRuntime? javaRuntime, List<JavaRuntimeObject>? javaRuntimeObjects)
    {
        return ObjectValidator<JavaRuntime>.IsNotNull(javaRuntime)
            && ObjectValidator<JavaRuntimeObject>.IsNotNullOrEmpty(javaRuntimeObjects)
            && ObjectValidator<string>.IsNotNullOrWhiteSpace(
                [javaRuntimeObjects?.FirstOrDefault()?.JavaRuntimeManifest?.Url]
            );
    }
}
