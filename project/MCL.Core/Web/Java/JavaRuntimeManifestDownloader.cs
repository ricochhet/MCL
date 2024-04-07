using System;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Java;

public static class JavaRuntimeManifestDownloader
{
    public static async Task<bool> Download(
        string minecraftPath,
        JavaRuntimePlatformEnum javaRuntimePlatformEnum,
        JavaRuntimeTypeEnum javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    )
    {
        if (javaRuntimeIndex == null)
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

        string downloadPath = MinecraftPathResolver.DownloadedJavaRuntimeManifestPath(
            minecraftPath,
            JavaRuntimeTypeEnumResolver.ToString(javaRuntimeTypeEnum)
        );
        string javaRuntimeManifest = await Request.DoRequest(url, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(javaRuntimeManifest))
            return false;
        return true;
    }

    private static string GetJavaRuntimeUrl(JavaRuntimeTypeEnum javaRuntimeTypeEnum, JavaRuntime javaRuntimePlatform)
    {
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
}
