using System;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;

namespace MCL.Core.Helpers;

public class DownloadProvider
{
    public VersionManifest versionManifest = new();
    public VersionDetails versionDetails = new();
    public Models.Version version;
    public AssetsData assets = new();
    private static string minecraftPath;
    private static string minecraftVersion;
    private static PlatformEnum minecraftPlatform;
    private static JsonSerializerOptions options;

    public DownloadProvider(string _minecraftPath, string _minecraftVersion, PlatformEnum _minecraftPlatform)
    {
        minecraftPath = _minecraftPath;
        minecraftVersion = _minecraftVersion;
        minecraftPlatform = _minecraftPlatform;

        options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<bool> RequestDownloads()
    {
        if (!await DownloadHelper.DownloadVersionManifestJson(minecraftPath))
        {
            LogBase.Error($"Failed to download version manifest");
            return false;
        }

        versionManifest = Json.Read<VersionManifest>(MinecraftPath.DownloadedVersionManifestPath(minecraftPath));
        try
        {
            version = VersionHelper.GetVersion(minecraftVersion, versionManifest.Versions);
        }
        catch (Exception ex)
        {
            LogBase.Error($"Failed to get version: {ex.Message}");
            return false;
        }

        if (!await DownloadHelper.DownloadVersionDetailsJson(minecraftPath, version))
        {
            LogBase.Error($"Failed to download version details");
            return false;
        }

        versionDetails = Json.Read<VersionDetails>(MinecraftPath.DownloadedVersionDetailsPath(minecraftPath, version));
        if (!await DownloadHelper.DownloadLibraries(minecraftPath, minecraftPlatform, versionDetails.Libraries))
        {
            LogBase.Error("Failed to download libraries");
            return false;
        }

        if (!await DownloadHelper.DownloadClient(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download client");
            return false;
        }

        if (!await DownloadHelper.DownloadServer(minecraftPath, versionDetails))
        {
            LogBase.Error("Failed to download server");
            return false;
        }

        assets = await WebRequest.DoRequest<AssetsData>(versionDetails.AssetIndex.URL, options);

        if (!await DownloadHelper.DownloadIndexJson(minecraftPath, versionDetails.AssetIndex))
        {
            LogBase.Error("Failed to download assets index json");
            return false;
        }

        if (!await DownloadHelper.DownloadResources(minecraftPath, MinecraftUrl.MinecraftResourcesUrl, assets))
        {
            LogBase.Error("Failed to download resources");
            return false;
        }

        return true;
    }
}
