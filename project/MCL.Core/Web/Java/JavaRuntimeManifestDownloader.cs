using System;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Extensions.Java;
using MCL.Core.Handlers.Java;
using MCL.Core.Interfaces.Web.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Web.Java;

public class JavaRuntimeManifestDownloader : IJavaRuntimeManifestDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimePlatform javaRuntimePlatformEnum,
        JavaRuntimeType javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!javaRuntimeIndex.JavaRuntimeExists())
            return false;

        string request = javaRuntimePlatformEnum switch
        {
            JavaRuntimePlatform.GAMECORE => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Gamecore),
            JavaRuntimePlatform.LINUX => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Linux),
            JavaRuntimePlatform.LINUXI386 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.LinuxI386),
            JavaRuntimePlatform.MACOS => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.Macos),
            JavaRuntimePlatform.MACOSARM64 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.MacosArm64),
            JavaRuntimePlatform.WINDOWSARM64 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsArm64),
            JavaRuntimePlatform.WINDOWSX64 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsX64),
            JavaRuntimePlatform.WINDOWSX86 => GetJavaRuntimeUrl(javaRuntimeTypeEnum, javaRuntimeIndex.WindowsX86),
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(javaRuntimePlatformEnum),
                    "Invalid Java runtime platform."
                ),
        };

        if (string.IsNullOrWhiteSpace(request))
            return false;

        string javaRuntimeFiles = await Request.GetJsonAsync<JavaRuntimeFiles>(
            request,
            JavaPathResolver.DownloadedJavaRuntimeManifestPath(
                launcherPath,
                JavaRuntimeTypeResolver.ToString(javaRuntimeTypeEnum)
            ),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(javaRuntimeFiles))
            return false;
        return true;
    }

    public static string GetJavaRuntimeUrl(JavaRuntimeType javaRuntimeTypeEnum, JavaRuntime javaRuntime)
    {
        if (!javaRuntime.JavaRuntimeExists())
            return default;

        return javaRuntimeTypeEnum switch
        {
            JavaRuntimeType.JAVA_RUNTIME_ALPHA => javaRuntime.JavaRuntimeAlpha[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_BETA => javaRuntime.JavaRuntimeBeta[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_DELTA => javaRuntime.JavaRuntimeDelta[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA => javaRuntime.JavaRuntimeGamma[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA_SNAPSHOT
                => javaRuntime.JavaRuntimeGammaSnapshot[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JRE_LEGACY => javaRuntime.JreLegacy[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.MINECRAFT_JAVA_EXE => javaRuntime.MinecraftJavaExe[0].JavaRuntimeManifest.Url,
            _ => throw new ArgumentOutOfRangeException(nameof(javaRuntimeTypeEnum), "Invalid Java runtime type."),
        };
    }
}
