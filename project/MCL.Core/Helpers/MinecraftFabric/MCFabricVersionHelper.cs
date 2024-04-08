using System.Collections.Generic;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public static class MCFabricVersionHelper
{
    public static MCFabricInstaller GetFabricInstallerVersion(
        MCLauncherVersion fabricInstallerVersion,
        List<MCFabricInstaller> installers
    )
    {
        if (!MCLauncherVersion.Exists(fabricInstallerVersion))
            return null;

        foreach (MCFabricInstaller item in installers)
        {
            if (item.Version == fabricInstallerVersion.FabricInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCFabricLoader GetFabricLoaderVersion(
        MCLauncherVersion fabricLoaderVersion,
        List<MCFabricLoader> loaders
    )
    {
        if (!MCLauncherVersion.Exists(fabricLoaderVersion))
            return null;

        foreach (MCFabricLoader item in loaders)
        {
            if (item.Version == fabricLoaderVersion.FabricLoaderVersion)
                return item;
        }
        return null;
    }
}
