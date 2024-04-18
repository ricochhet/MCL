using MCL.Core.Java.Models;

namespace MCL.Core.Java.Extensions;

public static class JavaVersionManifestExt
{
    public static bool JavaRuntimeExists(this JavaVersionManifest javaVersionManifest)
    {
        if (javaVersionManifest?.Gamecore == null)
            return false;

        if (javaVersionManifest.Linux == null || javaVersionManifest.LinuxI386 == null)
            return false;

        if (javaVersionManifest.Macos == null || javaVersionManifest.MacosArm64 == null)
            return false;

        return javaVersionManifest.WindowsArm64 != null
            && javaVersionManifest.WindowsX64 != null
            && javaVersionManifest.WindowsX86 != null;
    }
}
