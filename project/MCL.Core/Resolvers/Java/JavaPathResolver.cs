using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Minecraft;

public static class JavaPathResolver
{
    public static string JavaRuntimePath(MCLauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "runtime");

    public static string DownloadedJavaRuntimeIndexPath(MCLauncherPath launcherPath) =>
        VFS.Combine(JavaRuntimePath(launcherPath), "java_runtime_index.json");

    public static string DownloadedJavaRuntimeManifestPath(MCLauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, "java_runtime_manifest.json");

    public static string DownloadedJavaRuntimePath(MCLauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
}
