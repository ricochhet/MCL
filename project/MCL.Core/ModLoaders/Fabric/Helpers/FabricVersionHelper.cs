using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.ModLoaders.Fabric.Extensions;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricVersionHelper
{
    public static async Task<bool> SetVersions(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        FabricInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.FabricUrls);
        if (!FabricInstallerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await FabricInstallerDownloadService.DownloadVersionManifest();
            FabricInstallerDownloadService.LoadVersionManifest();
        }

        if (FabricInstallerDownloadService.FabricVersionManifest == null)
            return false;

        List<string> installerVersions = GetInstallerVersionIds(FabricInstallerDownloadService.FabricVersionManifest);
        List<string> loaderVersions = GetLoaderVersionIds(FabricInstallerDownloadService.FabricVersionManifest);
        string installerVersion = launcherVersion.FabricInstallerVersion;
        string loaderVersion = launcherVersion.FabricLoaderVersion;

        if (installerVersion == "latest" || string.IsNullOrWhiteSpace(installerVersion))
            installerVersion = installerVersions[0];

        if (loaderVersion == "latest" || string.IsNullOrWhiteSpace(loaderVersion))
            loaderVersion = loaderVersions[0];

        if (!installerVersions.Contains(installerVersion) || !loaderVersions.Contains(loaderVersion))
            return false;

        settings.LauncherVersion.FabricInstallerVersion = installerVersion;
        settings.LauncherVersion.FabricLoaderVersion = loaderVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetInstallerVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (!fabricVersionManifest.InstallerExists())
            return [];

        List<string> versions = [];
        foreach (FabricInstaller item in fabricVersionManifest.Installer)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static List<string> GetLoaderVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (!fabricVersionManifest.LoaderExists())
            return [];

        List<string> versions = [];
        foreach (FabricLoader item in fabricVersionManifest.Loader)
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    public static FabricInstaller GetInstallerVersion(
        LauncherVersion installerVersion,
        FabricVersionManifest fabricVersionManifest
    )
    {
        if (!installerVersion.VersionsExists())
            return null;

        if (!fabricVersionManifest.InstallerExists())
            return null;

        FabricInstaller fabricInstaller = fabricVersionManifest.Installer[0];
        foreach (FabricInstaller item in fabricVersionManifest.Installer)
        {
            if (item.Version == installerVersion.FabricInstallerVersion)
                return item;
        }
        return fabricInstaller;
    }

    public static FabricLoader GetLoaderVersion(
        LauncherVersion loaderVersion,
        FabricVersionManifest fabricVersionManifest
    )
    {
        if (!loaderVersion.VersionsExists())
            return null;

        if (!fabricVersionManifest.LoaderExists())
            return null;

        FabricLoader fabricLoader = fabricVersionManifest.Loader[0];
        foreach (FabricLoader item in fabricVersionManifest.Loader)
        {
            if (item.Version == loaderVersion.FabricLoaderVersion)
                return item;
        }
        return fabricLoader;
    }
}
