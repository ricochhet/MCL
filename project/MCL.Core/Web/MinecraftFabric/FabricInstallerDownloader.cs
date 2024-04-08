using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class FabricInstallerDownloader : IFabricInstallerDownloader
{
    public static async Task<bool> Download(MCLauncherPath fabricInstallerPath, MCFabricInstaller fabricInstaller)
    {
        if (!MCLauncherPath.Exists(fabricInstallerPath))
            return false;

        if (!Exists(fabricInstaller))
            return false;

        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (
            FsProvider.Exists(
                MinecraftFabricPathResolver.DownloadedFabricInstallerPath(fabricInstallerPath, fabricInstaller)
            )
        )
        {
            LogBase.Info($"Fabric version: {fabricInstaller.Version} is already downloaded.");
            return true;
        }

        return await Request.Download(
            fabricInstaller.URL,
            MinecraftFabricPathResolver.DownloadedFabricInstallerPath(fabricInstallerPath, fabricInstaller)
        );
    }

    public static bool Exists(MCFabricInstaller fabricInstaller)
    {
        if (fabricInstaller == null)
            return false;

        if (string.IsNullOrEmpty(fabricInstaller.URL))
            return false;

        if (string.IsNullOrEmpty(fabricInstaller.Version))
            return false;

        return true;
    }
}
