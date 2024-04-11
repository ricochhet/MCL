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

    public async Task<bool> DownloadFabricProfile()
    {
        if (!await FabricProfileDownloader.Download(launcherPath, launcherVersion, fabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricProfileDownloader)])
            );
            return false;
        }

        fabricProfile = Json.Load<MCFabricProfile>(
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(launcherPath, launcherVersion)
        );
        if (fabricProfile == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(MCFabricProfile)])
            );
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadFabricLoader()
    {
        MCFabricLoader fabricLoader = MCFabricVersionHelper.GetFabricLoaderVersion(launcherVersion, fabricIndex);
        if (fabricLoader == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [launcherVersion?.FabricLoaderVersion, nameof(MCFabricLoader)]
                )
            );
            return false;
        }

        if (!await FabricLoaderDownloader.Download(launcherPath, launcherVersion, fabricProfile, fabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricLoaderDownloader)])
            );
            return false;
        }

        return true;
    }
}
