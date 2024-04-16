using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Java;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class LibraryDownloader : ILibraryDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherSettings launcherSettings,
        MCVersionDetails versionDetails
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!versionDetails.LibrariesExists())
            return false;

        string libPath = VFS.FromCwd(launcherPath.Path, "libraries");
        foreach (MCLibrary lib in versionDetails.Libraries)
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

    public static bool SkipLibrary(MCLibrary lib, MCLauncherSettings launcherSettings)
    {
        if (lib.Rules == null)
            return false;

        if (lib.Rules.Count <= 0)
            return false;

        bool allowLibrary = false;
        foreach (MCLibraryRule rule in lib.Rules)
        {
            string action = rule.Action;
            string os = rule.Os?.Name;

            if (os == null)
            {
                allowLibrary = action == RuleResolver.ToString(Rule.ALLOW);
                continue;
            }

            if (os == JavaRuntimePlatformResolver.ToPlatformString(launcherSettings.JavaRuntimePlatform))
            {
                allowLibrary = action == RuleResolver.ToString(Rule.ALLOW);
            }
        }

        return !allowLibrary;
    }

    public static async Task<bool> DownloadNatives(
        MCLauncherPath launcherPath,
        MCLibrary lib,
        MCLauncherSettings launcherSettings
    )
    {
        if (lib.Downloads.Classifiers == null)
            return true;

        string classifierFilePath = string.Empty;
        string classifierUrl = string.Empty;
        string classifierSha1 = string.Empty;
        string libraryPath = MinecraftPathResolver.LibraryPath(launcherPath);

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
