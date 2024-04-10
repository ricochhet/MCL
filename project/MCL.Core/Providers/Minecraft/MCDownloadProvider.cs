using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.Minecraft;

public class MCDownloadProvider(
    MCLauncherPath _launcherPath,
    MCLauncherVersion _launcherVersion,
    Platform _platform,
    MCConfigUrls _configUrls
)
{
    private MCVersionDetails versionDetails;
    private MCVersion version;
    private MCAssetsData assets;
    private readonly MCLauncherPath launcherPath = _launcherPath;
    private readonly MCLauncherVersion launcherVersion = _launcherVersion;
    private readonly Platform platform = _platform;
    private readonly MCConfigUrls configUrls = _configUrls;

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadVersionManifest())
            return false;

        if (!await DownloadVersionDetails())
            return false;

        if (!await DownloadLibraries())
            return false;

        if (!await DownloadClient())
            return false;

        if (!await DownloadClientMappings())
            return false;

        if (!await DownloadServer())
            return false;

        if (!await DownloadServerMappings())
            return false;

        if (!await DownloadAssetIndex())
            return false;

        if (!await DownloadResources())
            return false;

        if (!await DownloadLogging())
            return false;

        return true;
    }

    public async Task<bool> DownloadVersionManifest()
    {
        if (!await VersionManifestDownloader.Download(launcherPath, configUrls))
        {
            LogBase.Error("Failed to download version manifest");
            return false;
        }

        MCVersionManifest versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );
        if (versionManifest == null)
        {
            LogBase.Info($"Failed to get version manifest");
            return false;
        }

        version = MCVersionHelper.GetVersion(launcherVersion, versionManifest.Versions);
        if (version == null)
        {
            LogBase.Error($"Failed to get version: {version}");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadVersionDetails()
    {
        if (!await VersionDetailsDownloader.Download(launcherPath, version))
        {
            LogBase.Error("Failed to download version details");
            return false;
        }

        versionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );
        if (versionDetails == null)
        {
            LogBase.Error($"Failed to get version details");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadLibraries()
    {
        if (!await LibraryDownloader.Download(launcherPath, platform, versionDetails.Libraries))
        {
            LogBase.Error("Failed to download libraries");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadClient()
    {
        if (!await ClientDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download client");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadClientMappings()
    {
        if (!await ClientMappingsDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download client mappings");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadServer()
    {
        if (!await ServerDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download server");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadServerMappings()
    {
        if (!await ServerMappingsDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download server mappings");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadAssetIndex()
    {
        if (!await IndexDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download assets index json");
            return false;
        }

        assets = Json.Load<MCAssetsData>(MinecraftPathResolver.ClientIndexPath(launcherPath, versionDetails));
        if (assets == null)
        {
            LogBase.Error($"Failed to get assets index json");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadResources()
    {
        if (!await ResourceDownloader.Download(launcherPath, configUrls, assets))
        {
            LogBase.Error("Failed to download resources");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadLogging()
    {
        if (!await LoggingDownloader.Download(launcherPath, versionDetails))
        {
            LogBase.Error("Failed to download logging");
            return false;
        }

        return true;
    }
}
