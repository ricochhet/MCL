using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class JavaRuntimeTypeEnumResolver
{
    public static string ToString(JavaRuntimeTypeEnum type) =>
        type switch
        {
            JavaRuntimeTypeEnum.JAVA_RUNTIME_ALPHA => "java-runtime-alpha",
            JavaRuntimeTypeEnum.JAVA_RUNTIME_BETA => "java-runtime-beta",
            JavaRuntimeTypeEnum.JAVA_RUNTIME_DELTA => "java-runtime-gamma",
            JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA => "java-runtime-gamma",
            JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA_SNAPSHOT => "java-runtime-gamma-snapshot",
            JavaRuntimeTypeEnum.JRE_LEGACY => "jre-legacy",
            JavaRuntimeTypeEnum.MINECRAFT_JAVA_EXE => "minecraft-java-exe",
            _ => throw new NotImplementedException(),
        };
}
