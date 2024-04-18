using System;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Extensions.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Web.Java;

public static class JavaRuntimeManifestDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        JavaRuntimePlatform javaRuntimePlatform,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimeIndex javaRuntimeIndex
    )
    {
        if (!javaRuntimeIndex.JavaRuntimeExists())
            return false;

        string request = javaRuntimePlatform switch
        {
            JavaRuntimePlatform.GAMECORE => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.Gamecore),
            JavaRuntimePlatform.LINUX => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.Linux),
            JavaRuntimePlatform.LINUXI386 => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.LinuxI386),
            JavaRuntimePlatform.MACOS => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.Macos),
            JavaRuntimePlatform.MACOSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.MacosArm64),
            JavaRuntimePlatform.WINDOWSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.WindowsArm64),
            JavaRuntimePlatform.WINDOWSX64 => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.WindowsX64),
            JavaRuntimePlatform.WINDOWSX86 => GetJavaRuntimeUrl(javaRuntimeType, javaRuntimeIndex.WindowsX86),
            _ => throw new ArgumentOutOfRangeException(nameof(javaRuntimePlatform), "Invalid Java runtime platform."),
        };

        if (string.IsNullOrWhiteSpace(request))
            return false;

        string javaRuntimeFiles = await Request.GetJsonAsync<JavaRuntimeFiles>(
            request,
            JavaPathResolver.DownloadedJavaRuntimeManifestPath(
                launcherPath,
                JavaRuntimeTypeResolver.ToString(javaRuntimeType)
            ),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(javaRuntimeFiles))
            return false;
        return true;
    }

    public static string GetJavaRuntimeUrl(JavaRuntimeType javaRuntimeType, JavaRuntime javaRuntime)
    {
        if (!javaRuntime.JavaRuntimeExists())
            return default;

        return javaRuntimeType switch
        {
            JavaRuntimeType.JAVA_RUNTIME_ALPHA => javaRuntime.JavaRuntimeAlpha[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_BETA => javaRuntime.JavaRuntimeBeta[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_DELTA => javaRuntime.JavaRuntimeDelta[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA => javaRuntime.JavaRuntimeGamma[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JAVA_RUNTIME_GAMMA_SNAPSHOT
                => javaRuntime.JavaRuntimeGammaSnapshot[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.JRE_LEGACY => javaRuntime.JreLegacy[0].JavaRuntimeManifest.Url,
            JavaRuntimeType.MINECRAFT_JAVA_EXE => javaRuntime.MinecraftJavaExe[0].JavaRuntimeManifest.Url,
            _ => throw new ArgumentOutOfRangeException(nameof(javaRuntimeType), "Invalid Java runtime type."),
        };
    }
}
