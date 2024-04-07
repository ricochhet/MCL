using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Interfaces.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Java;

public class JavaRuntimeManifestDownloader : IJavaRuntimeManifestDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath minecraftPath,
        JavaRuntimePlatformEnum javaRuntimePlatformEnum,
        JavaRuntimeTypeEnum javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(javaRuntimeIndex))
            return false;

        string url = javaRuntimePlatformEnum switch
        {
            JavaRuntimePlatformEnum.GAMECORE => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Gamecore),
            JavaRuntimePlatformEnum.LINUX => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Linux),
            JavaRuntimePlatformEnum.LINUXI386 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.LinuxI386),
            JavaRuntimePlatformEnum.MACOS => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Macos),
            JavaRuntimePlatformEnum.MACOSARM64 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.MacosArm64),
            JavaRuntimePlatformEnum.WINDOWSARM64
                => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsArm64),
            JavaRuntimePlatformEnum.WINDOWSX64 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsX64),
            JavaRuntimePlatformEnum.WINDOWSX86 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsX86),
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(javaRuntimePlatformEnum),
                    "Invalid Java runtime platform."
                ),
        };

        if (string.IsNullOrEmpty(url))
            return false;

        string javaRuntimeManifest = await Request.DoRequest(
            url,
            MinecraftPathResolver.DownloadedJavaRuntimeManifestPath(
                minecraftPath,
                JavaRuntimeTypeEnumResolver.ToString(javaRuntimeTypeEnum)
            ),
            Encoding.UTF8
        );
        if (string.IsNullOrEmpty(javaRuntimeManifest))
            return false;
        return true;
    }

    public static bool Exists(JavaRuntimeIndex javaRuntimeIndex)
    {
        if (javaRuntimeIndex == null)
            return false;

        if (javaRuntimeIndex.Gamecore == null)
            return false;

        if (javaRuntimeIndex.Linux == null)
            return false;

        if (javaRuntimeIndex.LinuxI386 == null)
            return false;

        if (javaRuntimeIndex.Macos == null)
            return false;

        if (javaRuntimeIndex.MacosArm64 == null)
            return false;

        if (javaRuntimeIndex.WindowsArm64 == null)
            return false;

        if (javaRuntimeIndex.WindowsX64 == null)
            return false;

        if (javaRuntimeIndex.WindowsX86 == null)
            return false;

        return true;
    }

    public static string GetJavaRuntimeUrl(JavaRuntimeTypeEnum javaRuntimeTypeEnum, JavaRuntime javaRuntimePlatform)
    {
        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JavaRuntimeAlpha))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JavaRuntimeBeta))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JavaRuntimeDelta))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JavaRuntimeGamma))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JavaRuntimeGammaSnapshot))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.JreLegacy))
            return default;

        if (!JavaRuntimeUrlExists(javaRuntimePlatform, javaRuntimePlatform.MinecraftJavaExe))
            return default;

        return javaRuntimeTypeEnum switch
        {
            JavaRuntimeTypeEnum.JAVA_RUNTIME_ALPHA => javaRuntimePlatform.JavaRuntimeAlpha[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.JAVA_RUNTIME_BETA => javaRuntimePlatform.JavaRuntimeBeta[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.JAVA_RUNTIME_DELTA => javaRuntimePlatform.JavaRuntimeDelta[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA => javaRuntimePlatform.JavaRuntimeGamma[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA_SNAPSHOT
                => javaRuntimePlatform.JavaRuntimeGammaSnapshot[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.JRE_LEGACY => javaRuntimePlatform.JreLegacy[0].JavaRuntimeManifest.Url,
            JavaRuntimeTypeEnum.MINECRAFT_JAVA_EXE => javaRuntimePlatform.MinecraftJavaExe[0].JavaRuntimeManifest.Url,
            _ => throw new ArgumentOutOfRangeException(nameof(javaRuntimeTypeEnum), "Invalid Java runtime type."),
        };
    }

    public static bool JavaRuntimeUrlExists(JavaRuntime javaRuntimePlatform, List<JavaRuntimeObject> javaRuntimeObjects)
    {
        if (javaRuntimePlatform == null)
            return false;

        if (javaRuntimeObjects == null)
            return false;

        if (javaRuntimeObjects.Count <= 0)
            return false;

        if (javaRuntimeObjects[0].JavaRuntimeManifest == null)
            return false;

        if (string.IsNullOrEmpty(javaRuntimeObjects[0].JavaRuntimeManifest.Url))
            return false;

        return true;
    }
}
