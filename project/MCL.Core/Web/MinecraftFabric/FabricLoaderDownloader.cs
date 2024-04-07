using System.Threading.Tasks;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class FabricLoaderDownloader
{
    public static async Task<bool> Download(string fabricPath, MCFabricInstaller fabricInstaller)
    {
        if (
            fabricInstaller == null
            || string.IsNullOrEmpty(fabricInstaller?.URL)
            || string.IsNullOrEmpty(fabricInstaller?.Version)
        )
            return false;

        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (FsProvider.Exists(MinecraftFabricPathResolver.DownloadedFabricLoaderPath(fabricPath, fabricInstaller)))
        {
            LogBase.Info($"Fabric version: {fabricInstaller.Version} is already downloaded.");
            return true;
        }

        return await Request.Download(
            fabricInstaller.URL,
            MinecraftFabricPathResolver.DownloadedFabricLoaderPath(fabricPath, fabricInstaller)
        );
    }
}
