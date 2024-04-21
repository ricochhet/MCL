using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricVersionHelper
{
    public static async Task<bool> SetInstallerVersion(
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

        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricInstallerDownloadService.FabricVersionManifest))
            return false;

        List<string> installerVersions = GetInstallerVersionIds(FabricInstallerDownloadService.FabricVersionManifest);
        string installerVersion = launcherVersion.FabricInstallerVersion;

        if (installerVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion]))
            installerVersion = installerVersions[0];

        if (!installerVersions.Contains(installerVersion))
            return false;

        settings.LauncherVersion.FabricInstallerVersion = installerVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static async Task<bool> SetLoaderVersion(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        FabricLoaderDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherInstance,
            settings.FabricUrls
        );
        if (!FabricLoaderDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await FabricLoaderDownloadService.DownloadVersionManifest();
            FabricLoaderDownloadService.LoadVersionManifest();
        }

        if (ObjectValidator<FabricVersionManifest>.IsNull(FabricLoaderDownloadService.FabricVersionManifest))
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(FabricLoaderDownloadService.FabricVersionManifest);
        string loaderVersion = launcherVersion.FabricLoaderVersion;

        if (loaderVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion]))
            loaderVersion = loaderVersions[0];

        if (!loaderVersions.Contains(loaderVersion))
            return false;

        settings.LauncherVersion.FabricLoaderVersion = loaderVersion;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetInstallerVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (ObjectValidator<List<FabricInstaller>>.IsNullOrEmpty(fabricVersionManifest?.Installer))
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
        if (ObjectValidator<List<FabricLoader>>.IsNullOrEmpty(fabricVersionManifest?.Loader))
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
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.FabricInstallerVersion])
            || ObjectValidator<List<FabricInstaller>>.IsNullOrEmpty(fabricVersionManifest?.Installer)
        )
            return null;

        FabricInstaller fabricInstaller = fabricVersionManifest.Installer[0];
        if (ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.FabricInstallerVersion]))
            return fabricInstaller;

        foreach (FabricInstaller item in fabricVersionManifest.Installer)
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([installerVersion?.FabricInstallerVersion])
                && item.Version == installerVersion.FabricInstallerVersion
            )
                return item;
        }
        return fabricInstaller;
    }

    public static FabricLoader GetLoaderVersion(
        LauncherVersion loaderVersion,
        FabricVersionManifest fabricVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.FabricLoaderVersion])
            || ObjectValidator<List<FabricLoader>>.IsNullOrEmpty(fabricVersionManifest?.Loader)
        )
            return null;

        FabricLoader fabricLoader = fabricVersionManifest.Loader[0];
        if (ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.FabricLoaderVersion]))
            return fabricLoader;

        foreach (FabricLoader item in fabricVersionManifest.Loader)
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([loaderVersion?.FabricLoaderVersion])
                && item.Version == loaderVersion.FabricLoaderVersion
            )
                return item;
        }
        return fabricLoader;
    }
}
