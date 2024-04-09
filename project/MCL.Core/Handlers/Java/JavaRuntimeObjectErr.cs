using System.Collections.Generic;
using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeObjectErr
{
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

        if (javaRuntimeObjects[0].JavaRuntimeManifest == null)
            return false;

        if (string.IsNullOrWhiteSpace(javaRuntimeObjects[0].JavaRuntimeManifest.Url))
            return false;

        return true;
    }
}
