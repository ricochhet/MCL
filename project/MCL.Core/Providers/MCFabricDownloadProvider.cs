using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers;

public class MCFabricDownloadProvider
{
    public MCFabricIndex fabricIndex = new();
    private static MCLauncherPath fabricPath;
    private static MCLauncherVersion fabricVersion;
    private static MCFabricConfigUrls fabricUrls;
    private static MCFabricInstaller fabricInstaller;

    public MCFabricDownloadProvider(
        MCLauncherPath _fabricPath,
        MCLauncherVersion _fabricVersion,
        MCFabricConfigUrls _fabricUrls
    )
    {
        fabricPath = _fabricPath;
        fabricVersion = _fabricVersion;
        fabricUrls = _fabricUrls;
    }

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadFabricIndex())
            return false;

        if (!await DownloadFabricLoader())
            return false;

        return true;
    }

    public async Task<bool> DownloadFabricIndex()
    {
        if (!await FabricIndexDownloader.Download(fabricPath, fabricUrls))
        {
            LogBase.Error("Failed to download fabric index");
            return false;
        }

        fabricIndex = Json.Read<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(fabricPath));
        if (fabricIndex == null)
        {
            LogBase.Error($"Failed to get fabric index");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricLoader()
    {
        fabricInstaller = VersionHelper.GetFabricVersion(fabricVersion, fabricIndex.Installer);
        if (fabricInstaller == null)
        {
            LogBase.Error($"Failed to get version: {fabricVersion}");
            return false;
        }

        if (!await FabricLoaderDownloader.Download(fabricPath, fabricInstaller))
        {
            LogBase.Error("Failed to download fabric loader");
            return false;
        }

        return true;
    }
}
