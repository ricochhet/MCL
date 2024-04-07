using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers;

public class MCDownloadProvider
{
    public MCVersionManifest versionManifest = new();
    public MCVersionDetails versionDetails = new();
    public MCVersion version;
    public MCAssetsData assets = new();
    private static string minecraftPath;
    private static string minecraftVersion;
    private static PlatformEnum minecraftPlatform;
    private static MCConfigUrls minecraftUrls;

    public MCDownloadProvider(
        string _minecraftPath,
        string _minecraftVersion,
        PlatformEnum _minecraftPlatform,
        MCConfigUrls _minecraftUrls
    )
    {
        minecraftPath = _minecraftPath;
        minecraftVersion = _minecraftVersion;
        minecraftPlatform = _minecraftPlatform;
        minecraftUrls = _minecraftUrls;
    }

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

        if (!await DownloadServer())
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
        if (!await VersionManifestDownloader.Download(minecraftUrls, minecraftPath))
        {
            LogBase.Error("Failed to download version manifest");
            return false;
        }

        versionManifest = Json.Read<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );

        version = VersionHelper.GetVersion(minecraftVersion, versionManifest.Versions);
        if (version == null)
        {
            LogBase.Error($"Failed to get version: {version}");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadVersionDetails()
    {
        if (!await VersionDetailsDownloader.Download(minecraftPath, version))
        {
            LogBase.Error("Failed to download version details");
            return false;
        }

        versionDetails = Json.Read<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );

        return true;
    }

    public async Task<bool> DownloadLibraries()
    {
        if (!await LibraryDownloader.Download(minecraftPath, minecraftPlatform, versionDetails.Libraries))
        {
            LogBase.Error("Failed to download libraries");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadClient()
    {
        if (!await ClientDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download client");
            return false;
        }

        if (!await ClientDownloader.DownloadMappings(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download client mappings");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadServer()
    {
        if (!await ServerDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download server");
            return false;
        }

        if (!await ServerDownloader.DownloadMappings(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download server mappings");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadAssetIndex()
    {
        if (!await IndexDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download assets index json");
            return false;
        }

        assets = Json.Read<MCAssetsData>(MinecraftPathResolver.ClientIndexPath(minecraftPath, versionDetails));

        return true;
    }

    public async Task<bool> DownloadResources()
    {
        if (!await ResourceDownloader.Download(minecraftPath, minecraftUrls, assets))
        {
            LogBase.Error("Failed to download resources");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadLogging()
    {
        if (!await LoggingDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download logging");
            return false;
        }

        return true;
    }
}
