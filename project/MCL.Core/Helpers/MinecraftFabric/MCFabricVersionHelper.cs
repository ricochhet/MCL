using MCL.Core.Handlers.MinecraftFabric;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public class MCFabricVersionHelper : IMCFabricVersionHelper<MCFabricInstaller, MCFabricLoader, MCFabricIndex>
{
    public static MCFabricInstaller GetInstallerVersion(MCLauncherVersion installerVersion, MCFabricIndex index)
    {
        if (!MCLauncherVersion.Exists(installerVersion))
            return null;

        if (!MCFabricVersionHelperErr.Exists(index))
            return null;

        foreach (MCFabricInstaller item in index.Installer)
        {
            if (item.Version == installerVersion.FabricInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCFabricLoader GetLoaderVersion(MCLauncherVersion loaderVersion, MCFabricIndex index)
    {
        if (!MCLauncherVersion.Exists(loaderVersion))
            return null;

        if (!MCFabricVersionHelperErr.Exists(index))
            return null;

        foreach (MCFabricLoader item in index.Loader)
        {
            if (item.Version == loaderVersion.FabricLoaderVersion)
                return item;
        }
        return null;
    }
}
