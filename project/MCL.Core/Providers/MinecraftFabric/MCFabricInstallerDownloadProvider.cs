using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricInstallerDownloadProvider(
    MCLauncherPath _launcherPath,
    MCLauncherVersion _launcherVersion,
    MCFabricConfigUrls _fabricConfigUrls
)
{
    private MCFabricIndex fabricIndex;
    private readonly MCLauncherPath launcherPath = _launcherPath;
    private readonly MCLauncherVersion launcherVersion = _launcherVersion;
    private readonly MCFabricConfigUrls fabricConfigUrls = _fabricConfigUrls;

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
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricIndexDownloader)])
            );
            return false;
        }

        fabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(launcherPath));
        if (fabricIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]));
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricInstaller()
    {
        MCFabricInstaller fabricInstaller = MCFabricVersionHelper.GetFabricInstallerVersion(
            launcherVersion,
            fabricIndex
        );
        if (fabricInstaller == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [launcherVersion?.FabricInstallerVersion, nameof(MCFabricInstaller)]
                )
            );
            return false;
        }

        if (!await FabricInstallerDownloader.Download(launcherPath, fabricInstaller))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricInstallerDownloader)])
            );
            return false;
        }

        return true;
    }
}
