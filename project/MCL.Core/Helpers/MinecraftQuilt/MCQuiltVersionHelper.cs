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

        MCQuiltInstaller quiltInstaller = index.Installer[0];
        if (string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
            return quiltInstaller;

        foreach (MCQuiltInstaller item in index.Installer)
        {
            if (
                (!string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
                && item.Version == installerVersion.QuiltInstallerVersion
            )
                return item;
        }
        return quiltInstaller;
    }

    public static MCQuiltLoader GetLoaderVersion(MCLauncherVersion loaderVersion, MCQuiltIndex index)
    {
        if (!MCLauncherVersion.Exists(loaderVersion))
            return null;

        if (!MCQuiltVersionHelperErr.Exists(index))
            return null;

        MCQuiltLoader quiltLoader = index.Loader[0];
        if (string.IsNullOrWhiteSpace(loaderVersion.QuiltLoaderVersion))
            return quiltLoader;

        foreach (MCQuiltLoader item in index.Loader)
        {
            if (
                (!string.IsNullOrWhiteSpace(loaderVersion.QuiltLoaderVersion))
                && item.Version == loaderVersion.QuiltLoaderVersion
            )
                return item;
        }
        return quiltLoader;
    }
}
