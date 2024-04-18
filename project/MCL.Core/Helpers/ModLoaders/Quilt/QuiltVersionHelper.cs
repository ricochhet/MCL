using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Quilt;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.ModLoaders.Quilt;

namespace MCL.Core.Helpers.ModLoaders.Quilt;

public static class QuiltVersionHelper
{
    public static async Task<bool> SetVersions(Settings settings, string[] args)
    {
        QuiltInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
        if (!QuiltInstallerDownloadService.LoadIndex())
        {
            await QuiltInstallerDownloadService.DownloadIndex();
            QuiltInstallerDownloadService.LoadIndex();
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

        settings.LauncherVersion.QuiltInstallerVersion = installerVersion;
        settings.LauncherVersion.QuiltLoaderVersion = loaderVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetInstallerVersionIds(QuiltIndex index)
    {
        if (!index.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (QuiltInstaller item in index.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(QuiltIndex index)
    {
        if (!index.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (QuiltLoader item in index.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static QuiltInstaller GetInstallerVersion(LauncherVersion installerVersion, QuiltIndex index)
    {
        if (!installerVersion.VersionsExists())
            return null;

        if (!index.InstallerExists())
            return null;

        QuiltInstaller quiltInstaller = index.Installer[0];
        if (string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
            return quiltInstaller;

        foreach (QuiltInstaller item in index.Installer)
        {
            if (
                (!string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
                && item.Version == installerVersion.QuiltInstallerVersion
            )
                return item;
        }
        return quiltInstaller;
    }

    public static QuiltLoader GetLoaderVersion(LauncherVersion loaderVersion, QuiltIndex index)
    {
        if (!loaderVersion.VersionsExists())
            return null;

        if (!index.LoaderExists())
            return null;

        QuiltLoader quiltLoader = index.Loader[0];
        if (string.IsNullOrWhiteSpace(loaderVersion.QuiltLoaderVersion))
            return quiltLoader;

        foreach (QuiltLoader item in index.Loader)
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
