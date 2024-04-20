using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricInstallerDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricInstaller fabricInstaller
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                launcherVersion?.FabricInstallerVersion,
                fabricInstaller?.URL,
                fabricInstaller?.Version
            )
        )
            return false;

        string fabricInstallerPath = FabricPathResolver.InstallerPath(launcherPath, launcherVersion);
        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(fabricInstallerPath))
        {
            NotificationService.Log(NativeLogLevel.Info, "fabric.installer-exists", fabricInstaller?.Version);
            return true;
        }

        return await Request.DownloadSHA1(fabricInstaller.URL, fabricInstallerPath, string.Empty);
    }
}
