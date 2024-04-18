using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Fabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Resolvers.ModLoaders.Fabric;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Web.ModLoaders.Fabric;

public static class FabricInstallerDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricInstaller fabricInstaller
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;

        if (!fabricInstaller.UrlExists() || !fabricInstaller.VersionExists())
            return false;

        string fabricInstallerPath = FabricPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion);
        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(fabricInstallerPath))
        {
            NotificationService.Log(NativeLogLevel.Info, "fabric.installer-exists", [fabricInstaller?.Version]);
            return true;
        }

        return await Request.Download(fabricInstaller.URL, fabricInstallerPath, string.Empty);
    }
}
