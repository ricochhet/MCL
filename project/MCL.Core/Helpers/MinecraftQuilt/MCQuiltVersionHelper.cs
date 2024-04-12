using MCL.Core.Handlers.MinecraftQuilt;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public static class MCQuiltVersionHelper
{
    public static MCQuiltInstaller GetQuiltInstallerVersion(
        MCLauncherVersion quiltInstallerVersion,
        MCQuiltIndex quiltIndex
    )
    {
        if (!MCLauncherVersion.Exists(quiltInstallerVersion))
            return null;

        if (!MCQuiltVersionHelperErr.Exists(quiltIndex))
            return null;

        foreach (MCQuiltInstaller item in quiltIndex.Installer)
        {
            if (item.Version == quiltInstallerVersion.QuiltInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCQuiltLoader GetQuiltLoaderVersion(MCLauncherVersion quiltLoaderVersion, MCQuiltIndex quiltIndex)
    {
        if (!MCLauncherVersion.Exists(quiltLoaderVersion))
            return null;

        if (!MCQuiltVersionHelperErr.Exists(quiltIndex))
            return null;

        foreach (MCQuiltLoader item in quiltIndex.Loader)
        {
            if (item.Version == quiltLoaderVersion.QuiltLoaderVersion)
                return item;
        }
        return null;
    }
}
