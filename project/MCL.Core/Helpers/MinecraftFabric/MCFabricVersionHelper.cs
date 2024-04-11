using MCL.Core.Handlers.Minecraft;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public static class MCFabricVersionHelper
{
    public static MCFabricInstaller GetFabricInstallerVersion(
        MCLauncherVersion fabricInstallerVersion,
        MCFabricIndex fabricIndex
    )
    {
        if (!MCLauncherVersion.Exists(fabricInstallerVersion))
            return null;

        if (!MCFabricVersionHelperErr.Exists(fabricIndex))
            return null;

        foreach (MCFabricInstaller item in fabricIndex.Installer)
        {
            if (item.Version == fabricInstallerVersion.FabricInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCFabricLoader GetFabricLoaderVersion(
        MCLauncherVersion fabricLoaderVersion,
        MCFabricIndex fabricIndex
    )
    {
        if (!MCLauncherVersion.Exists(fabricLoaderVersion))
            return null;

        if (!MCFabricVersionHelperErr.Exists(fabricIndex))
            return null;

        foreach (MCFabricLoader item in fabricIndex.Loader)
        {
            if (item.Version == fabricLoaderVersion.FabricLoaderVersion)
                return item;
        }
        return null;
    }
}
