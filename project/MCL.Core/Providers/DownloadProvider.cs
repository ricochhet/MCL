using System;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Config.Minecraft;
using MCL.Core.Enums;
using MCL.Core.Helpers;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers;

public class DownloadProvider
{
    public VersionManifest versionManifest = new();
    public VersionDetails versionDetails = new();
    public Models.Minecraft.Version version;
    public AssetsData assets = new();
    private static string minecraftPath;
    private static string minecraftVersion;
    private static PlatformEnum minecraftPlatform;
    private static JsonSerializerOptions options;
    private static MinecraftUrls minecraftUrls;

    public DownloadProvider(
        string _minecraftPath,
        string _minecraftVersion,
        PlatformEnum _minecraftPlatform,
        MinecraftUrls _minecraftUrls
    )
    {
        minecraftPath = _minecraftPath;
        minecraftVersion = _minecraftVersion;
        minecraftPlatform = _minecraftPlatform;
        minecraftUrls = _minecraftUrls;

        options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<bool> RequestDownloads()
    {
        if (!await VersionManifestDownloader.Download(minecraftUrls, minecraftPath))
        {
            LogBase.Error($"Failed to download version manifest");
            return false;
        }

        versionManifest = Json.Read<VersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );
        try
        {
            version = VersionHelper.GetVersion(minecraftVersion, versionManifest.Versions);
        }
        catch (Exception ex)
        {
            LogBase.Error($"Failed to get version: {ex.Message}");
            return false;
        }

        if (!await VersionDetailsDownloader.Download(minecraftPath, version))
        {
            LogBase.Error($"Failed to download version details");
            return false;
        }

        versionDetails = Json.Read<VersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );
        if (!await LibraryDownloader.Download(minecraftPath, minecraftPlatform, versionDetails.Libraries))
        {
            LogBase.Error("Failed to download libraries");
            return false;
        }

        if (!await ClientDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download client");
            return false;
        }

        if (!await ServerDownloader.Download(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download server");
            return false;
        }

        assets = await Request.DoRequest<AssetsData>(versionDetails.AssetIndex.URL, options);
        if (!await IndexDownloader.Download(minecraftPath, versionDetails.AssetIndex))
        {
            LogBase.Error("Failed to download assets index json");
            return false;
        }

        if (!await ResourceDownloader.Download(minecraftPath, minecraftUrls.MinecraftResources, assets))
        {
            LogBase.Error("Failed to download resources");
            return false;
        }

        return true;
    }
}
