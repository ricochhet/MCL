using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricLoaderDownloadProvider
{
    public MCFabricIndex fabricIndex = new();
    private static MCLauncherPath launcherPath;
    private static MCLauncherVersion launcherVersion;
    private static MCFabricConfigUrls fabricConfigUrls;
    private static MCFabricProfile fabricProfile;

    public MCFabricLoaderDownloadProvider(
        MCLauncherPath _launcherPath,
        MCLauncherVersion _launcherVersion,
        MCFabricConfigUrls _fabricConfigUrls
    )
    {
        launcherPath = _launcherPath;
        launcherVersion = _launcherVersion;
        fabricConfigUrls = _fabricConfigUrls;
    }

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadFabricProfile())
            return false;

        if (!await DownloadFabricLoader())
            return false;

        return true;
    }

    public async Task<bool> DownloadFabricProfile()
    {
        if (!await FabricProfileDownloader.Download(launcherPath, launcherVersion, fabricConfigUrls))
        {
            LogBase.Error("Failed to download fabric profile");
            return false;
        }

        fabricProfile = Json.Read<MCFabricProfile>(
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(launcherPath, launcherVersion)
        );
        if (fabricProfile == null)
        {
            LogBase.Error($"Failed to get fabric profile");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricLoader()
    {
        if (!await FabricLoaderDownloader.Download(launcherPath, launcherVersion, fabricProfile, fabricConfigUrls))
        {
            LogBase.Error("Failed to download fabric loader");
            return false;
        }

        return true;
    }
}
