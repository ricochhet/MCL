using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class LibraryDownloader : IMCLibraryDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        PlatformEnum platform,
        List<MCLibrary> libraries
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        string libPath = VFS.Combine(launcherPath.Path, "libraries");
        foreach (MCLibrary lib in libraries)
        {
            if (lib.Downloads == null)
                return false;

            if (SkipLibrary(lib, platform))
                continue;

            if (!await DownloadNatives(launcherPath, lib, platform))
                return false;

            if (!LibraryDownloaderErr.Exists(lib))
                return false;

            string downloadPath = VFS.Combine(libPath, lib.Downloads.Artifact.Path);
            if (!await Request.Download(lib.Downloads.Artifact.URL, downloadPath, lib.Downloads.Artifact.SHA1))
                return false;
        }

        return true;
    }

    public static bool SkipLibrary(MCLibrary lib, PlatformEnum platform)
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
            LogBase.Info($"Library Rule:\nAction: {action}\nOS: {os}");

            if (os == null)
            {
                allowLibrary = action == RuleEnumResolver.ToString(RuleEnum.ALLOW);
                continue;
            }

            if (os == PlatformEnumResolver.ToString(platform))
            {
                allowLibrary = action == RuleEnumResolver.ToString(RuleEnum.ALLOW);
                continue;
            }
        }

        return !allowLibrary;
    }

    public static async Task<bool> DownloadNatives(MCLauncherPath launcherPath, MCLibrary lib, PlatformEnum platform)
    {
        if (lib.Downloads.Classifiers == null)
            return true;

        string classifierDownloadPath = string.Empty;
        string classifierUrl = string.Empty;
        string classifierSha1 = string.Empty;

        switch (platform)
        {
            case PlatformEnum.WINDOWS:
                if (!LibraryNativesDownloaderErr.WindowsClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = VFS.Combine(
                    MinecraftPathResolver.LibraryPath(launcherPath),
                    lib.Downloads.Classifiers.NativesWindows.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesWindows.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesWindows.SHA1;
                break;
            case PlatformEnum.LINUX:
                if (!LibraryNativesDownloaderErr.LinuxClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = VFS.Combine(
                    MinecraftPathResolver.LibraryPath(launcherPath),
                    lib.Downloads.Classifiers.NativesLinux.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesLinux.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesLinux.SHA1;
                break;
            case PlatformEnum.OSX:
                if (!LibraryNativesDownloaderErr.OSXClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = VFS.Combine(
                    MinecraftPathResolver.LibraryPath(launcherPath),
                    lib.Downloads.Classifiers.NativesMacos.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesMacos.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesMacos.SHA1;
                break;
        }

        if (!await Request.Download(classifierUrl, classifierDownloadPath, classifierSha1))
            return false;
        return true;
    }
}
