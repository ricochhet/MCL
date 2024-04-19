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
        Instance instance,
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
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
        libraries = libraries.Prepend(MPathResolver.ClientLibrary(launcherVersion)).ToArray();

        switch (launcherSettings.ClientType)
        {
            case ClientType.VANILLA:
                libraries = ManageVanillaLibraries(libraries, instance);
                break;
            case ClientType.FABRIC:
                libraries = ManageFabricLibraries(libraries, instance, launcherVersion);
                break;
            case ClientType.QUILT:
                libraries = ManageQuiltLibraries(libraries, instance, launcherVersion);
                break;
        }

        return string.Join(
            separator,
            libraries.Select(lib => lib.Replace("\\", "/").Replace(launcherPath.Path + "/", string.Empty))
        );
    }

    private static string[] ManageVanillaLibraries(string[] libraries, Instance instance)
    {
        string[] managedLibraries = libraries;

        foreach (var loader in instance.FabricLoaders.Concat(instance.QuiltLoaders))
        {
            libraries = libraries.Except(loader.Libraries).ToArray();
        }

        return managedLibraries;
    }

    private static string[] ManageFabricLibraries(
        string[] libraries,
        Instance instance,
        LauncherVersion launcherVersion
    )
    {
        string[] managedLibraries = libraries;

        // Remove all fabric specific libraries that don't belong to the current version.
        foreach (InstanceModLoader loader in instance.FabricLoaders)
        {
            if (loader.LoaderVersion != launcherVersion.FabricLoaderVersion)
            {
                managedLibraries = libraries.Except(loader.Libraries).ToArray();
            }
        }

        // Remove all quilt specific libraries.
        foreach (InstanceModLoader loader in instance.QuiltLoaders)
        {
            managedLibraries = libraries.Except(loader.Libraries).ToArray();
        }

        return managedLibraries;
    }

    private static string[] ManageQuiltLibraries(string[] libraries, Instance instance, LauncherVersion launcherVersion)
    {
        string[] managedLibraries = libraries;

        // Remove all quilt specific libraries that don't belong to the current version.
        foreach (InstanceModLoader loader in instance.QuiltLoaders)
        {
            if (loader.LoaderVersion != launcherVersion.QuiltLoaderVersion)
            {
                managedLibraries = libraries.Except(loader.Libraries).ToArray();
            }
        }

        // Remove all fabric specific libraries.
        foreach (InstanceModLoader loader in instance.FabricLoaders)
        {
            managedLibraries = libraries.Except(loader.Libraries).ToArray();
        }

        return managedLibraries;
    }
}
