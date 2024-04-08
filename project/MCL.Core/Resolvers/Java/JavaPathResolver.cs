using System.Collections.Generic;
using System.IO;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;

namespace MCL.Core.Resolvers.Minecraft;

public static class JavaPathResolver
{
    public static string JavaRuntimePath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.MCPath, "runtime");
    }

    public static string DownloadedJavaRuntimeIndexPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(JavaRuntimePath(launcherPath), "java_runtime_index.json");
    }

    public static string DownloadedJavaRuntimeManifestPath(MCLauncherPath launcherPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, "java_runtime_manifest.json");
    }

    public static string DownloadedJavaRuntimePath(MCLauncherPath launcherPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
    }
}
