using MCL.Core.Handlers.MinecraftQuilt;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public class MCQuiltVersionHelper : IMCFabricVersionHelper<MCQuiltInstaller, MCQuiltLoader, MCQuiltIndex>
{
    public static MCQuiltInstaller GetInstallerVersion(MCLauncherVersion installerVersion, MCQuiltIndex index)
    {
        if (!MCLauncherVersion.Exists(installerVersion))
            return null;

        if (!MCQuiltVersionHelperErr.Exists(index))
            return null;

        foreach (MCQuiltInstaller item in index.Installer)
        {
            if (item.Version == installerVersion.QuiltInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCQuiltLoader GetLoaderVersion(MCLauncherVersion loaderVersion, MCQuiltIndex index)
    {
        if (!MCLauncherVersion.Exists(loaderVersion))
            return null;

        if (!MCQuiltVersionHelperErr.Exists(index))
            return null;

        foreach (MCQuiltLoader item in index.Loader)
        {
            if (item.Version == loaderVersion.QuiltLoaderVersion)
                return item;
        }
        return null;
    }
}
