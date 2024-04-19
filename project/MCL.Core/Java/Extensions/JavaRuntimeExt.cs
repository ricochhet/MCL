using System.Collections.Generic;
using MCL.Core.Java.Models;

namespace MCL.Core.Java.Extensions;

public static class JavaRuntimeExt
{
    public static bool ObjectsExists(this JavaRuntime javaRuntime, List<JavaRuntimeObject> javaRuntimeObjects)
    {
        if (javaRuntime == null)
            return false;

        if (javaRuntimeObjects == null || javaRuntimeObjects.Count <= 0)
            return false;

        return !string.IsNullOrWhiteSpace(javaRuntimeObjects[0]?.JavaRuntimeManifest?.Url);
    }
}
