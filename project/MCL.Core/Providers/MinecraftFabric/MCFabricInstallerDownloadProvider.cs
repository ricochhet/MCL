using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricInstallerDownloadProvider
{
    public MCFabricIndex fabricIndex;
    private static MCLauncherPath launcherPath;
    private static MCLauncherVersion launcherVersion;
    private static MCFabricConfigUrls fabricConfigUrls;
    private static MCFabricInstaller fabricInstaller;

    public MCFabricInstallerDownloadProvider(
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
        if (!await DownloadFabricIndex())
            return false;

        if (!await DownloadFabricInstaller())
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

    public async Task<bool> DownloadFabricInstaller()
    {
        fabricInstaller = MCFabricVersionHelper.GetFabricInstallerVersion(launcherVersion, fabricIndex.Installer);
        if (fabricInstaller == null)
        {
            LogBase.Error($"Failed to get version: {launcherVersion}");
            return false;
        }

        if (!await FabricInstallerDownloader.Download(launcherPath, fabricInstaller))
        {
            LogBase.Error("Failed to download fabric installer");
            return false;
        }

        return true;
    }
}
