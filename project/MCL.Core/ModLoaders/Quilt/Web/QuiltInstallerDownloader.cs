using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltInstallerDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltInstaller quiltInstaller
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;

        if (!quiltInstaller.UrlExists() || !quiltInstaller.VersionExists())
            return false;

        string quiltInstallerPath = QuiltPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion);
        // Quilt does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(quiltInstallerPath))
        {
            NotificationService.Log(NativeLogLevel.Info, "quilt.installer-exists", [quiltInstaller?.Version]);
            return true;
        }

        return await Request.Download(quiltInstaller.URL, quiltInstallerPath, string.Empty);
    }
}
