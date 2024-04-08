using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers;

public class MCFabricInstallerDownloadProvider
{
    public MCFabricIndex fabricIndex = new();
    private static MCLauncherPath fabricInstallerPath;
    private static MCLauncherVersion fabricInstallerVersion;
    private static MCFabricConfigUrls fabricUrls;
    private static MCFabricInstaller fabricInstaller;

    public MCFabricInstallerDownloadProvider(
        MCLauncherPath _fabricInstallerPath,
        MCLauncherVersion _fabricInstallerVersion,
        MCFabricConfigUrls _fabricUrls
    )
    {
        fabricInstallerPath = _fabricInstallerPath;
        fabricInstallerVersion = _fabricInstallerVersion;
        fabricUrls = _fabricUrls;
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
        if (!await FabricIndexDownloader.Download(fabricInstallerPath, fabricUrls))
        {
            LogBase.Error("Failed to download fabric index");
            return false;
        }

        fabricIndex = Json.Read<MCFabricIndex>(
            MinecraftFabricPathResolver.DownloadedFabricIndexPath(fabricInstallerPath)
        );
        if (fabricIndex == null)
        {
            LogBase.Error($"Failed to get fabric index");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricInstaller()
    {
        fabricInstaller = VersionHelper.GetFabricInstallerVersion(fabricInstallerVersion, fabricIndex.Installer);
        if (fabricInstaller == null)
        {
            LogBase.Error($"Failed to get version: {fabricInstallerVersion}");
            return false;
        }

        if (!await FabricInstallerDownloader.Download(fabricInstallerPath, fabricInstaller))
        {
            LogBase.Error("Failed to download fabric installer");
            return false;
        }

        return true;
    }
}
