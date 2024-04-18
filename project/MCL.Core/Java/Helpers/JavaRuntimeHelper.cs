using System;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Helpers;

public static class JavaRuntimeHelper
{
    public static string FindJavaRuntimeEnvironment(
        Settings settings,
        string workingDirectory,
        JavaRuntimeType javaRuntimeType
    )
    {
        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);

        if (!VFS.Exists(javaHome))
        {
            string javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                settings.JavaSettings.JavaHomeEnvironmentVariable
            );
            if (string.IsNullOrWhiteSpace(javaHomeEnvironmentVariable))
                return javaHome;
            return javaHomeEnvironmentVariable;
        }

        return javaHome;
    }
}
