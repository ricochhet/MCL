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
    private MCFabricIndex fabricIndex;
    private readonly MCLauncherPath launcherPath;
    private readonly MCLauncherVersion launcherVersion;
    private readonly MCFabricConfigUrls fabricConfigUrls;

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
        MCFabricInstaller fabricInstaller = MCFabricVersionHelper.GetFabricInstallerVersion(
            launcherVersion,
            fabricIndex.Installer
        );
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
