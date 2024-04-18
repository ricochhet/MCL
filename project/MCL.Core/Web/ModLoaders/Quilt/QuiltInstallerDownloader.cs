using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Quilt;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Resolvers.ModLoaders.Quilt;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Web.ModLoaders.Quilt;

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
