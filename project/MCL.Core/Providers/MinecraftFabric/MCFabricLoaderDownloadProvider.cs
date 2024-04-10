using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricLoaderDownloadProvider(
    MCLauncherPath _launcherPath,
    MCLauncherVersion _launcherVersion,
    MCFabricConfigUrls _fabricConfigUrls
)
{
    private MCFabricIndex fabricIndex;
    private MCFabricProfile fabricProfile;
    private readonly MCLauncherPath launcherPath = _launcherPath;
    private readonly MCLauncherVersion launcherVersion = _launcherVersion;
    private readonly MCFabricConfigUrls fabricConfigUrls = _fabricConfigUrls;

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadFabricIndex())
            return false;

        if (!await DownloadFabricProfile())
            return false;

        if (!await DownloadFabricLoader())
            return false;

        return true;
    }

    public async Task<bool> DownloadFabricIndex()
    {
        if (!await FabricIndexDownloader.Download(launcherPath, fabricConfigUrls))
        {
            LogBase.Error("Failed to download fabric index");
            return false;
        }

        fabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(launcherPath));
        if (fabricIndex == null)
        {
            LogBase.Error($"Failed to get fabric index");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricProfile()
    {
        if (!await FabricProfileDownloader.Download(launcherPath, launcherVersion, fabricConfigUrls))
        {
            LogBase.Error("Failed to download fabric profile");
            return false;
        }

        fabricProfile = Json.Load<MCFabricProfile>(
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
        MCFabricLoader fabricLoader = MCFabricVersionHelper.GetFabricLoaderVersion(launcherVersion, fabricIndex.Loader);
        if (fabricLoader == null)
        {
            LogBase.Error($"Failed to get version: {launcherVersion}");
            return false;
        }

        if (!await FabricLoaderDownloader.Download(launcherPath, launcherVersion, fabricProfile, fabricConfigUrls))
        {
            LogBase.Error("Failed to download fabric loader");
            return false;
        }

        return true;
    }
}
