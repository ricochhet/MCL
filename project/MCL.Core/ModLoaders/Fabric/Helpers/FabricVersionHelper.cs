using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.ModLoaders.Fabric.Extensions;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricVersionHelper
{
    public static async Task<bool> SetVersions(Settings settings, string[] args)
    {
        FabricInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.FabricUrls);
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

        settings.LauncherVersion.FabricInstallerVersion = installerVersion;
        settings.LauncherVersion.FabricLoaderVersion = loaderVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetInstallerVersionIds(FabricIndex index)
    {
        if (!index.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (FabricInstaller item in index.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(FabricIndex index)
    {
        if (!index.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (FabricLoader item in index.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static FabricInstaller GetInstallerVersion(LauncherVersion installerVersion, FabricIndex index)
    {
        if (!installerVersion.VersionsExists())
            return null;

        if (!index.InstallerExists())
            return null;

        FabricInstaller fabricInstaller = index.Installer[0];
        foreach (FabricInstaller item in index.Installer)
        {
            if (item.Version == installerVersion.FabricInstallerVersion)
                return item;
        }
        return fabricInstaller;
    }

    public static FabricLoader GetLoaderVersion(LauncherVersion loaderVersion, FabricIndex index)
    {
        if (!loaderVersion.VersionsExists())
            return null;

        if (!index.LoaderExists())
            return null;

        FabricLoader fabricLoader = index.Loader[0];
        foreach (FabricLoader item in index.Loader)
        {
            if (item.Version == loaderVersion.FabricLoaderVersion)
                return item;
        }
        return fabricLoader;
    }
}
