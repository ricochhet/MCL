using System.Collections.Generic;
using MCL.Core.Models.Java;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeManifestDownloaderErr
{
    public static bool Exists(JavaRuntimeIndex javaRuntimeIndex)
    {
        if (javaRuntimeIndex?.Gamecore == null)
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

    public static bool Exists(JavaRuntime javaRuntime)
    {
        if (!Exists(javaRuntime, javaRuntime.JavaRuntimeAlpha))
            return false;

        if (!Exists(javaRuntime, javaRuntime.JavaRuntimeBeta))
            return false;

        if (!Exists(javaRuntime, javaRuntime.JavaRuntimeDelta))
            return false;

        if (!Exists(javaRuntime, javaRuntime.JavaRuntimeGamma))
            return false;

        if (!Exists(javaRuntime, javaRuntime.JavaRuntimeGammaSnapshot))
            return false;

        if (!Exists(javaRuntime, javaRuntime.JreLegacy))
            return false;

        if (!Exists(javaRuntime, javaRuntime.MinecraftJavaExe))
            return false;

        return true;
    }

    public static bool Exists(JavaRuntime javaRuntime, List<JavaRuntimeObject> javaRuntimeObjects)
    {
        if (javaRuntime == null)
            return false;

        if (javaRuntimeObjects == null)
            return false;

        if (javaRuntimeObjects.Count <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(javaRuntimeObjects[0]?.JavaRuntimeManifest?.Url))
            return false;

        return true;
    }
}
