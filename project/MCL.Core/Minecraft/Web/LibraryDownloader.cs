using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Enums;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class LibraryDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherSettings launcherSettings,
        MVersionDetails versionDetails
    )
    {
        if (!versionDetails.LibrariesExists())
            return false;

        string libPath = VFS.Combine(launcherPath.Path, "libraries");
        foreach (MLibrary lib in versionDetails.Libraries)
        {
            if (lib.Downloads == null)
                return false;

            if (SkipLibrary(lib, launcherSettings))
                continue;

            if (!await DownloadNatives(launcherPath, lib, launcherSettings))
                return false;

            if (!lib.ArtifactExists())
                return false;

            string filepath = VFS.Combine(libPath, lib.Downloads.Artifact.Path);
            if (!await Request.Download(lib.Downloads.Artifact.URL, filepath, lib.Downloads.Artifact.SHA1))
                return false;
        }

        return true;
    }

    public static bool SkipLibrary(MLibrary lib, LauncherSettings launcherSettings)
    {
        if (lib.Rules == null)
            return false;

        if (lib.Rules.Count <= 0)
            return false;

        bool allowLibrary = false;
        foreach (MLibraryRule rule in lib.Rules)
        {
            string action = rule.Action;
            string os = rule.Os?.Name;

            if (os == null)
            {
                allowLibrary = action == RuleTypeResolver.ToString(RuleType.ALLOW);
                continue;
            }

            if (os == JavaRuntimePlatformResolver.ToPlatformString(launcherSettings.JavaRuntimePlatform))
            {
                allowLibrary = action == RuleTypeResolver.ToString(RuleType.ALLOW);
            }
        }

        return !allowLibrary;
    }

    public static async Task<bool> DownloadNatives(
        LauncherPath launcherPath,
        MLibrary lib,
        LauncherSettings launcherSettings
    )
    {
        if (lib.Downloads.Classifiers == null)
            return true;

        string classifierFilePath = string.Empty;
        string classifierUrl = string.Empty;
        string classifierSha1 = string.Empty;
        string libraryPath = MPathResolver.LibraryPath(launcherPath);

        switch (launcherSettings.JavaRuntimePlatform)
        {
            case JavaRuntimePlatform.WINDOWSX64
            or JavaRuntimePlatform.WINDOWSX86
            or JavaRuntimePlatform.WINDOWSARM64:
                if (!lib.WindowsClassifierNativesExists())
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesWindows.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesWindows.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesWindows.SHA1;
                break;
            case JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386:
                if (!lib.LinuxClassifierNativesExists())
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesLinux.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesLinux.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesLinux.SHA1;
                break;
            case JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64:
                if (!lib.OSXClassifierNativesExists())
                    return false;

                classifierFilePath = VFS.Combine(libraryPath, lib.Downloads.Classifiers.NativesMacos.Path);
                classifierUrl = lib.Downloads.Classifiers.NativesMacos.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesMacos.SHA1;
                break;
        }

        if (!await Request.Download(classifierUrl, classifierFilePath, classifierSha1))
            return false;
        return true;
    }
}