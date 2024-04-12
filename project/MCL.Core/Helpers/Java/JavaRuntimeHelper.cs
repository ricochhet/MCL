using System;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Helpers.Java;

public static class JavaRuntimeHelper
{
    public static string FindJavaRuntimeEnvironment(
        Config config,
        string workingDirectory,
        JavaRuntimeType javaRuntimeType
    )
    {
        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);

        if (!VFS.Exists(javaHome))
        {
            string javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                config.JavaConfig.JavaHomeEnvironmentVariable
            );
            if (string.IsNullOrWhiteSpace(javaHomeEnvironmentVariable))
                return javaHome;
            return javaHomeEnvironmentVariable;
        }

        return javaHome;
    }
}
