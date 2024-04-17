using System.Threading.Tasks;
using MCL.Core.Extensions.MinecraftQuilt;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Web.MinecraftQuilt;

public class QuiltInstallerDownloader : IFabricInstallerDownloader<MCQuiltInstaller>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltInstaller quiltInstaller
    )
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return false;

        if (!quiltInstaller.UrlExists() || !quiltInstaller.VersionExists())
            return false;

        string quiltInstallerPath = QuiltPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion);
        // Quilt does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(quiltInstallerPath))
        {
            NotificationService.Add(new(NativeLogLevel.Info, "quilt.installer-exists", [quiltInstaller?.Version]));
            return true;
        }

        return await Request.Download(quiltInstaller.URL, quiltInstallerPath, string.Empty);
    }
}
