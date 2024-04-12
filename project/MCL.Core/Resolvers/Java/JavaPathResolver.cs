using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Java;

public static class JavaPathResolver
{
    public static string JavaRuntimePath(MCLauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "runtime");

    public static string JavaRuntimeHome(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(workingDirectory, "runtime", JavaRuntimeTypeResolver.ToString(javaRuntimeType));

    public static string JavaRuntimeBin(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(JavaRuntimeHome(workingDirectory, javaRuntimeType), "bin");

    public static string JavaRuntimeBin(string workingDirectory) => VFS.Combine(workingDirectory, "bin");

    public static string DownloadedJavaRuntimeIndexPath(MCLauncherPath launcherPath) =>
        VFS.Combine(JavaRuntimePath(launcherPath), "java_runtime_index.json");

    public static string DownloadedJavaRuntimeManifestPath(MCLauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, "java_runtime_manifest.json");

    public static string DownloadedJavaRuntimePath(MCLauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
}
