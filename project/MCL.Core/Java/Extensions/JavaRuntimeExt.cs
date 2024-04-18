using System.Collections.Generic;
using MCL.Core.Java.Models;

namespace MCL.Core.Java.Extensions;

public static class JavaRuntimeExt
{
    public static bool JavaRuntimeExists(this JavaRuntime javaRuntime)
    {
        if (!ObjectsExists(javaRuntime, javaRuntime.JavaRuntimeAlpha))
            return false;

        if (!ObjectsExists(javaRuntime, javaRuntime.JavaRuntimeBeta))
            return false;

        if (!ObjectsExists(javaRuntime, javaRuntime.JavaRuntimeDelta))
            return false;

        if (!ObjectsExists(javaRuntime, javaRuntime.JavaRuntimeGamma))
            return false;

        if (!ObjectsExists(javaRuntime, javaRuntime.JavaRuntimeGammaSnapshot))
            return false;

        return !ObjectsExists(javaRuntime, javaRuntime.JreLegacy)
            || !ObjectsExists(javaRuntime, javaRuntime.MinecraftJavaExe);
    }

    private static bool ObjectsExists(JavaRuntime javaRuntime, List<JavaRuntimeObject> javaRuntimeObjects)
    {
        if (javaRuntime == null)
            return false;

        if (javaRuntimeObjects == null || javaRuntimeObjects.Count <= 0)
            return false;

        return !string.IsNullOrWhiteSpace(javaRuntimeObjects[0]?.JavaRuntimeManifest?.Url);
    }
}
