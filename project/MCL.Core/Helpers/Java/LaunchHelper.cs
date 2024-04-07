using System.Collections.Generic;
using System.IO;
using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Resolvers;

namespace MCL.Core.Helpers.Java;

public static class JavaLaunchHelper
{
    public static void Launch(JvmArguments jvmArguments, string workingDirectory)
    {
        ProcessHelper.RunProcess("java", jvmArguments.Build(), workingDirectory, false);
    }

    public static void Launch(JvmArguments jvmArguments, string workingDirectory, JavaRuntimeTypeEnum javaRuntimeType)
    {
        string javaBin = Path.Combine(
            workingDirectory,
            "runtime",
            JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType),
            "bin"
        );
        ProcessHelper.RunProcess(
            Path.Combine(javaBin, "java.exe"),
            jvmArguments.Build(),
            workingDirectory,
            false,
            new() { { "JAVA_HOME", javaBin } }
        );
    }
}
