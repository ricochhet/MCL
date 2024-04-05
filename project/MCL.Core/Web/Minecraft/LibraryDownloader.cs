using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class LibraryDownloader
{
    public static async Task<bool> Download(
        string minecraftPath,
        PlatformEnum minecraftPlatform,
        List<Library> libraries
    )
    {
        string libPath = Path.Combine(minecraftPath, "libraries");
        foreach (Library lib in libraries)
        {
            if (lib.Downloads == null)
                return false;

            if (SkipLibrary(lib, minecraftPlatform))
                continue;

            if (!await DownloadNatives(minecraftPath, lib, minecraftPlatform))
                return false;

            if (
                lib.Downloads.Artifact == null
                || string.IsNullOrEmpty(lib.Downloads.Artifact?.Path)
                || string.IsNullOrEmpty(lib.Downloads.Artifact?.URL)
                || string.IsNullOrEmpty(lib.Downloads.Artifact?.SHA1)
            )
                return false;

            string downloadPath = Path.Combine(libPath, lib.Downloads.Artifact.Path);
            return await Request.Download(downloadPath, lib.Downloads.Artifact.URL, lib.Downloads.Artifact.SHA1);
        }

        return true;
    }

    private static bool SkipLibrary(Library lib, PlatformEnum minecraftPlatform)
    {
        if (lib.Rules == null | lib?.Rules?.Count <= 0)
            return false;
        foreach (Rule rule in lib.Rules)
        {
            if (
                rule?.Action == RuleEnumResolver.ToString(RuleEnum.ALLOW)
                && rule?.Os?.Name != PlatformEnumResolver.ToString(minecraftPlatform)
            )
            {
                return true;
            }
            else if (
                rule?.Action == RuleEnumResolver.ToString(RuleEnum.DISALLOW)
                && rule?.Os?.Name == PlatformEnumResolver.ToString(minecraftPlatform)
            )
            {
                return true;
            }
        }
        return true;
    }

    private static async Task<bool> DownloadNatives(string minecraftPath, Library lib, PlatformEnum minecraftPlatform)
    {
        if (lib.Downloads?.Classifiers == null)
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

    private static bool WindowsClassifierNativesExists(Library lib) =>
        !(
            lib.Downloads.Classifiers.NativesWindows == null
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesWindows?.URL)
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesWindows?.SHA1)
        );

    private static bool LinuxClassifierNativesExists(Library lib) =>
        !(
            lib.Downloads.Classifiers.NativesLinux == null
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesLinux?.URL)
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesLinux?.SHA1)
        );

    private static bool OSXClassifierNativesExists(Library lib) =>
        !(
            lib.Downloads.Classifiers.NativesMacos == null
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesMacos?.URL)
            || string.IsNullOrEmpty(lib.Downloads.Classifiers.NativesMacos?.SHA1)
        );
}
