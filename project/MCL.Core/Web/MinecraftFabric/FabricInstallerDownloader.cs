using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services;

namespace MCL.Core.Web.Minecraft;

public class FabricInstallerDownloader : IFabricInstallerDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCFabricInstaller fabricInstaller)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!FabricInstallerDownloaderErr.Exists(fabricInstaller))
            return false;

        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(MinecraftFabricPathResolver.DownloadedFabricInstallerPath(launcherPath, fabricInstaller)))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Info, "fabric.installer-exists", [fabricInstaller?.Version])
            );
            return true;
        }

        return await Request.Download(
            fabricInstaller.URL,
            MinecraftFabricPathResolver.DownloadedFabricInstallerPath(launcherPath, fabricInstaller)
        );
    }
}
