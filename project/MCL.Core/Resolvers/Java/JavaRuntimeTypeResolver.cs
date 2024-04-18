using System;
using MCL.Core.Enums.Java;

namespace MCL.Core.Resolvers.Java;

public static class JavaRuntimeTypeResolver
{
    public static string ToString(JavaRuntimeType type) =>
        type switch
        {
            JavaRuntimeType.JAVA_RUNTIME_ALPHA => "java-runtime-alpha",
            JavaRuntimeType.JAVA_RUNTIME_BETA => "java-runtime-beta",
            JavaRuntimeType.JAVA_RUNTIME_DELTA => "java-runtime-gamma",
            JavaRuntimeType.JAVA_RUNTIME_GAMMA => "java-runtime-gamma",
            JavaRuntimeType.JAVA_RUNTIME_GAMMA_SNAPSHOT => "java-runtime-gamma-snapshot",
            JavaRuntimeType.JRE_LEGACY => "jre-legacy",
            JavaRuntimeType.MINECRAFT_JAVA_EXE => "minecraft-java-exe",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
