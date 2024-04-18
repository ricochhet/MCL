using System;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Web;

public static class JavaVersionDetailsDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        JavaRuntimePlatform javaRuntimePlatform,
        JavaRuntimeType javaRuntimeType,
        JavaVersionManifest javaVersionManifest
    )
    {
        if (!javaVersionManifest.JavaRuntimeExists())
            return false;

        string request = javaRuntimePlatform switch
        {
            JavaRuntimePlatform.GAMECORE => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.Gamecore),
            JavaRuntimePlatform.LINUX => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.Linux),
            JavaRuntimePlatform.LINUXI386 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.LinuxI386),
            JavaRuntimePlatform.MACOS => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.Macos),
            JavaRuntimePlatform.MACOSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.MacosArm64),
            JavaRuntimePlatform.WINDOWSARM64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.WindowsArm64),
            JavaRuntimePlatform.WINDOWSX64 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.WindowsX64),
            JavaRuntimePlatform.WINDOWSX86 => GetJavaRuntimeUrl(javaRuntimeType, javaVersionManifest.WindowsX86),
            _ => throw new ArgumentOutOfRangeException(nameof(javaRuntimePlatform), "Invalid Java runtime platform."),
        };

        if (string.IsNullOrWhiteSpace(request))
            return false;

        string javaRuntimeFiles = await Request.GetJsonAsync<JavaVersionDetails>(
            request,
            JavaPathResolver.DownloadedJavaVersionDetailsPath(
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
