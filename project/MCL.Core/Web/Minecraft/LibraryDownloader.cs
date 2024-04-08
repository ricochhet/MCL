using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Enums;
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
        MCLauncherPath minecraftPath,
        PlatformEnum minecraftPlatform,
        List<MCLibrary> libraries
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        string libPath = Path.Combine(minecraftPath.MCPath, "libraries");
        foreach (MCLibrary lib in libraries)
        {
            if (lib.Downloads == null)
                return false;

            if (SkipLibrary(lib, minecraftPlatform))
                continue;

            if (!await DownloadNatives(minecraftPath, lib, minecraftPlatform))
                return false;

            if (!Exists(lib))
                return false;

            string downloadPath = Path.Combine(libPath, lib.Downloads.Artifact.Path);
            if (!await Request.Download(downloadPath, lib.Downloads.Artifact.URL, lib.Downloads.Artifact.SHA1))
                return false;
        }

        return true;
    }

    public static bool Exists(MCLibrary lib)
    {
        if (lib.Downloads.Artifact == null)
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Artifact.Path))
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Artifact.URL))
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Artifact.SHA1))
            return false;

        return true;
    }

    public static bool SkipLibrary(MCLibrary lib, PlatformEnum minecraftPlatform)
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

            if (os == PlatformEnumResolver.ToString(minecraftPlatform))
            {
                allowLibrary = action == RuleEnumResolver.ToString(RuleEnum.ALLOW);
                continue;
            }
        }

        return !allowLibrary;
    }

    public static async Task<bool> DownloadNatives(
        MCLauncherPath minecraftPath,
        MCLibrary lib,
        PlatformEnum minecraftPlatform
    )
    {
        if (lib.Downloads.Classifiers == null)
            return true;

        string classifierDownloadPath = string.Empty;
        string classifierUrl = string.Empty;
        string classifierSha1 = string.Empty;

        switch (minecraftPlatform)
        {
            case PlatformEnum.WINDOWS:
                if (!WindowsClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = Path.Combine(
                    MinecraftPathResolver.LibraryPath(minecraftPath),
                    lib.Downloads.Classifiers.NativesWindows.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesWindows.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesWindows.SHA1;
                break;
            case PlatformEnum.LINUX:
                if (!LinuxClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = Path.Combine(
                    MinecraftPathResolver.LibraryPath(minecraftPath),
                    lib.Downloads.Classifiers.NativesLinux.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesLinux.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesLinux.SHA1;
                break;
            case PlatformEnum.OSX:
                if (!OSXClassifierNativesExists(lib))
                    return false;

                classifierDownloadPath = Path.Combine(
                    MinecraftPathResolver.LibraryPath(minecraftPath),
                    lib.Downloads.Classifiers.NativesMacos.Path
                );
                classifierUrl = lib.Downloads.Classifiers.NativesMacos.URL;
                classifierSha1 = lib.Downloads.Classifiers.NativesMacos.SHA1;
                break;
        }

        if (!await Request.Download(classifierDownloadPath, classifierUrl, classifierSha1))
            return false;
        return true;
    }

    public static bool WindowsClassifierNativesExists(MCLibrary lib)
    {
        if (lib.Downloads.Classifiers.NativesWindows == null)
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesWindows.URL))
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesWindows.SHA1))
            return false;

        return true;
    }

    public static bool LinuxClassifierNativesExists(MCLibrary lib)
    {
        if (lib.Downloads.Classifiers.NativesLinux == null)
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesLinux.URL))
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesLinux.SHA1))
            return false;

        return true;
    }

    public static bool OSXClassifierNativesExists(MCLibrary lib)
    {
        if (lib.Downloads.Classifiers.NativesMacos == null)
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesMacos.URL))
            return false;

        if (string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesMacos.SHA1))
            return false;

        return true;
    }
}
