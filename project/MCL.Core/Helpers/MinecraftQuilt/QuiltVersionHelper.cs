using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Extensions.MinecraftQuilt;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public class QuiltVersionHelper : IFabricVersionHelper<MCQuiltInstaller, MCQuiltLoader, MCQuiltIndex>
{
    public static async Task<bool> SetVersions(Config config, string[] args)
    {
        QuiltInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.QuiltUrls);
        if (!QuiltInstallerDownloadService.LoadIndex(true))
        {
            await QuiltInstallerDownloadService.DownloadIndex();
            QuiltInstallerDownloadService.LoadIndex(false);
        }

        if (QuiltInstallerDownloadService.QuiltIndex == null)
            return false;

        List<string> installerVersions = GetInstallerVersionIds(QuiltInstallerDownloadService.QuiltIndex);
        List<string> loaderVersions = GetLoaderVersionIds(QuiltInstallerDownloadService.QuiltIndex);
        string installerVersion = args[(int)VersionArgs.QUILT_INSTALLER];
        string loaderVersion = args[(int)VersionArgs.QUILT_LOADER];

        if (installerVersion == "latest")
            installerVersion = installerVersions[0];

        if (loaderVersion == "latest")
            loaderVersion = loaderVersions[0];

        if (!installerVersions.Contains(installerVersion) || !loaderVersions.Contains(loaderVersion))
            return false;

        config.LauncherVersion.QuiltInstallerVersion = installerVersion;
        config.LauncherVersion.QuiltLoaderVersion = loaderVersion;
        ConfigService.Save(config);
        return true;
    }

    public static List<string> GetInstallerVersionIds(MCQuiltIndex index)
    {
        if (!index.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (MCQuiltInstaller item in index.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(MCQuiltIndex index)
    {
        if (!index.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (MCQuiltLoader item in index.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static MCQuiltInstaller GetInstallerVersion(MCLauncherVersion installerVersion, MCQuiltIndex index)
    {
        if (!MCLauncherVersion.Exists(installerVersion))
            return null;

        if (!index.InstallerExists())
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

        if (!index.LoaderExists())
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
