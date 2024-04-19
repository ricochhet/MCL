using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public static class QuiltVersionHelper
{
    public static async Task<bool> SetInstallerVersion(
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
        string installerVersion = launcherVersion.QuiltInstallerVersion;

        if (installerVersion == "latest" || string.IsNullOrWhiteSpace(installerVersion))
            installerVersion = installerVersions[0];

        if (!installerVersions.Contains(installerVersion))
            return false;

        settings.LauncherVersion.QuiltInstallerVersion = installerVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static async Task<bool> SetLoaderVersion(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        QuiltLoaderDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherInstance,
            settings.QuiltUrls
        );
        if (!QuiltLoaderDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await QuiltLoaderDownloadService.DownloadVersionManifest();
            QuiltLoaderDownloadService.LoadVersionManifest();
        }

        if (QuiltLoaderDownloadService.QuiltVersionManifest == null)
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(QuiltLoaderDownloadService.QuiltVersionManifest);
        string loaderVersion = launcherVersion.QuiltLoaderVersion;

        if (loaderVersion == "latest" || string.IsNullOrWhiteSpace(loaderVersion))
            loaderVersion = loaderVersions[0];

        if (!loaderVersions.Contains(loaderVersion))
            return false;

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
        if (!installerVersion.QuiltInstallerVersionExists())
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
        if (!loaderVersion.QuiltLoaderVersionExists())
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
