using System;
using System.Linq;
using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ClassPathHelper
{
    public static string CreateClassPath(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        LauncherSettings launcherSettings
    )
    {
        if (!launcherVersion.VersionExists())
            return string.Empty;

        string separator = launcherSettings.JavaRuntimePlatform switch
        {
            JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386
            or JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64
                => ":",
            JavaRuntimePlatform.WINDOWSX64 or JavaRuntimePlatform.WINDOWSX86 or JavaRuntimePlatform.WINDOWSARM64 => ";",
            _ => throw new NotImplementedException("Unsupported OS."),
        };

        string libPath = VFS.Combine(launcherPath.Path, "libraries");
        string[] libraries = VFS.GetFiles(libPath, "*");
        libraries = libraries
            .Prepend(MPathResolver.ClientLibrary(launcherVersion))
            .Select(lib => lib.Replace("\\", "/"))
            .ToArray();

        switch (launcherSettings.ClientType)
        {
            case ClientType.VANILLA:
                libraries = ManageVanillaLibraries(libraries, launcherInstance);
                break;
            case ClientType.FABRIC:
                libraries = ManageFabricLibraries(libraries, launcherVersion, launcherInstance);
                break;
            case ClientType.QUILT:
                libraries = ManageQuiltLibraries(libraries, launcherVersion, launcherInstance);
                break;
        }

        string filepath = launcherPath.Path.Replace("\\", "/");
        return string.Join(
            separator,
            libraries.Select(lib => lib.Replace("\\", "/").Replace(filepath + "/", string.Empty))
        );
    }

    private static string[] ManageVanillaLibraries(string[] libraries, LauncherInstance launcherInstance)
    {
        string[] managedLibraries = libraries;

        foreach (LauncherModLoader loader in launcherInstance.FabricLoaders.Concat(launcherInstance.QuiltLoaders))
        {
            libraries = libraries.Except(loader.Libraries).ToArray();
        }

        return managedLibraries;
    }

    private static string[] ManageFabricLibraries(
        string[] libraries,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance
    )
    {
        string[] managedLibraries = libraries;

        // Remove all quilt specific libraries.
        foreach (LauncherModLoader loader in launcherInstance.QuiltLoaders)
        {
            managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
        }

        // Remove all fabric specific libraries that don't belong to the current version.
        foreach (LauncherModLoader loader in launcherInstance.FabricLoaders)
        {
            if (loader.Version != launcherVersion.FabricLoaderVersion)
                managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
            else
                managedLibraries = [.. managedLibraries, .. loader.Libraries];
        }

        return managedLibraries;
    }

    private static string[] ManageQuiltLibraries(
        string[] libraries,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance
    )
    {
        string[] managedLibraries = libraries;

        // Remove all fabric specific libraries.
        foreach (LauncherModLoader loader in launcherInstance.FabricLoaders)
        {
            managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
        }

        // Remove all quilt specific libraries that don't belong to the current version.
        foreach (LauncherModLoader loader in launcherInstance.QuiltLoaders)
        {
            if (loader.Version != launcherVersion.QuiltLoaderVersion)
                managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
            else
                managedLibraries = [.. managedLibraries, .. loader.Libraries];
        }

        return managedLibraries;
    }
}
