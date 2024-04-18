using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Extensions.MinecraftFabric;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public class FabricVersionHelper : IFabricVersionHelper<MCFabricInstaller, MCFabricLoader, MCFabricIndex>
{
    public static async Task<bool> SetVersions(Config config, string[] args)
    {
        FabricInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.FabricUrls);
        if (!FabricInstallerDownloadService.LoadIndex())
        {
            await FabricInstallerDownloadService.DownloadIndex();
            FabricInstallerDownloadService.LoadIndex();
        }

        if (FabricInstallerDownloadService.FabricIndex == null)
            return false;

        List<string> installerVersions = GetInstallerVersionIds(FabricInstallerDownloadService.FabricIndex);
        List<string> loaderVersions = GetLoaderVersionIds(FabricInstallerDownloadService.FabricIndex);
        string installerVersion = args[(int)VersionArgs.FABRIC_INSTALLER];
        string loaderVersion = args[(int)VersionArgs.FABRIC_LOADER];

        if (installerVersion == "latest")
            installerVersion = installerVersions[0];

        if (loaderVersion == "latest")
            loaderVersion = loaderVersions[0];

        if (!installerVersions.Contains(installerVersion) || !loaderVersions.Contains(loaderVersion))
            return false;

        config.LauncherVersion.FabricInstallerVersion = installerVersion;
        config.LauncherVersion.FabricLoaderVersion = loaderVersion;
        ConfigService.Save(config);
        return true;
    }

    public static List<string> GetInstallerVersionIds(MCFabricIndex index)
    {
        if (!index.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (MCFabricInstaller item in index.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(MCFabricIndex index)
    {
        if (!index.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (MCFabricLoader item in index.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static MCFabricInstaller GetInstallerVersion(MCLauncherVersion installerVersion, MCFabricIndex index)
    {
        if (!MCLauncherVersion.Exists(installerVersion))
            return null;

        if (!index.InstallerExists())
            return null;

        MCFabricInstaller fabricInstaller = index.Installer[0];
        foreach (MCFabricInstaller item in index.Installer)
        {
            if (item.Version == installerVersion.FabricInstallerVersion)
                return item;
        }
        return fabricInstaller;
    }

    public static MCFabricLoader GetLoaderVersion(MCLauncherVersion loaderVersion, MCFabricIndex index)
    {
        if (!MCLauncherVersion.Exists(loaderVersion))
            return null;

        if (!index.LoaderExists())
            return null;

        MCFabricLoader fabricLoader = index.Loader[0];
        foreach (MCFabricLoader item in index.Loader)
        {
            if (item.Version == loaderVersion.FabricLoaderVersion)
                return item;
        }
        return fabricLoader;
    }
}
