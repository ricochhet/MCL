using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public static class QuiltVersionHelper
{
    public static async Task<bool> SetVersions(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        QuiltInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
        if (!QuiltInstallerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await QuiltInstallerDownloadService.DownloadVersionManifest();
            QuiltInstallerDownloadService.LoadVersionManifest();
        }

        if (QuiltInstallerDownloadService.QuiltVersionManifest == null)
            return false;

        List<string> installerVersions = GetInstallerVersionIds(QuiltInstallerDownloadService.QuiltVersionManifest);
        List<string> loaderVersions = GetLoaderVersionIds(QuiltInstallerDownloadService.QuiltVersionManifest);
        string installerVersion = launcherVersion.QuiltInstallerVersion;
        string loaderVersion = launcherVersion.QuiltLoaderVersion;

        if (installerVersion == "latest" || string.IsNullOrWhiteSpace(installerVersion))
            installerVersion = installerVersions[0];

        if (loaderVersion == "latest" || string.IsNullOrWhiteSpace(loaderVersion))
            loaderVersion = loaderVersions[0];

        if (!installerVersions.Contains(installerVersion) || !loaderVersions.Contains(loaderVersion))
            return false;

        settings.LauncherVersion.QuiltInstallerVersion = installerVersion;
        settings.LauncherVersion.QuiltLoaderVersion = loaderVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetInstallerVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (!quiltVersionManifest.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (QuiltInstaller item in quiltVersionManifest.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (!quiltVersionManifest.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (QuiltLoader item in quiltVersionManifest.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static QuiltInstaller GetInstallerVersion(
        LauncherVersion installerVersion,
        QuiltVersionManifest quiltVersionManifest
    )
    {
        if (!installerVersion.VersionsExists())
            return null;

        if (!quiltVersionManifest.InstallerExists())
            return null;

        QuiltInstaller quiltInstaller = quiltVersionManifest.Installer[0];
        if (string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
            return quiltInstaller;

        foreach (QuiltInstaller item in quiltVersionManifest.Installer)
        {
            if (
                (!string.IsNullOrWhiteSpace(installerVersion.QuiltInstallerVersion))
                && item.Version == installerVersion.QuiltInstallerVersion
            )
                return item;
        }
        return quiltInstaller;
    }

    public static QuiltLoader GetLoaderVersion(LauncherVersion loaderVersion, QuiltVersionManifest quiltVersionManifest)
    {
        if (!loaderVersion.VersionsExists())
            return null;

        if (!quiltVersionManifest.LoaderExists())
            return null;

        QuiltLoader quiltLoader = quiltVersionManifest.Loader[0];
        if (string.IsNullOrWhiteSpace(loaderVersion.QuiltLoaderVersion))
            return quiltLoader;

        foreach (QuiltLoader item in quiltVersionManifest.Loader)
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
