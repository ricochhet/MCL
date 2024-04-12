using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftQuilt;

namespace MCL.Core.Web.Minecraft;

public class QuiltInstallerDownloader : IQuiltInstallerDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltInstaller quiltInstaller
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!QuiltInstallerDownloaderErr.Exists(quiltInstaller))
            return false;

        string quiltInstallerPath = MinecraftQuiltPathResolver.DownloadedQuiltInstallerPath(
            launcherPath,
            launcherVersion
        );
        // Quilt does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(quiltInstallerPath))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Info, "quilt.installer-exists", [quiltInstaller?.Version])
            );
            return true;
        }

        return await Request.Download(quiltInstaller.URL, quiltInstallerPath, string.Empty);
    }
}
