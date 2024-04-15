using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftFabric;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Web.MinecraftFabric;

public class FabricInstallerDownloader : IFabricInstallerDownloader<MCFabricInstaller>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricInstaller fabricInstaller
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!FabricInstallerDownloaderErr.Exists(fabricInstaller))
            return false;

        string fabricInstallerPath = FabricPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion);
        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(fabricInstallerPath))
        {
            NotificationService.Add(new(NativeLogLevel.Info, "fabric.installer-exists", [fabricInstaller?.Version]));
            return true;
        }

        return await Request.Download(fabricInstaller.URL, fabricInstallerPath, string.Empty);
    }
}
