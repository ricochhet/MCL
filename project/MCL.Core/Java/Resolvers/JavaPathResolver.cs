using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Resolvers;

public static class JavaPathResolver
{
    public static string JavaRuntimePath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "runtime");

    public static string JavaRuntimeHome(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(workingDirectory, "runtime", JavaRuntimeTypeResolver.ToString(javaRuntimeType));

    public static string JavaRuntimeBin(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(JavaRuntimeHome(workingDirectory, javaRuntimeType), "bin");

    public static string JavaRuntimeBin(string workingDirectory) => VFS.Combine(workingDirectory, "bin");

    public static string DownloadedJavaVersionManifestPath(LauncherPath launcherPath) =>
        VFS.Combine(JavaRuntimePath(launcherPath), "runtime_manifest.json");

    public static string DownloadedJavaVersionDetailsPath(LauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, $"runtime_mainfest-{javaRuntimeVersion}.json");

    public static string DownloadedJavaRuntimePath(LauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
}
