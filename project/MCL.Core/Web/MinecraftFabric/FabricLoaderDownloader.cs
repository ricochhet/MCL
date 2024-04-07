using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class FabricLoaderDownloader : IFabricLoaderDownloader
{
    public static async Task<bool> Download(string fabricPath, MCFabricInstaller fabricInstaller)
    {
        if (!Exists(fabricPath, fabricInstaller))
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

    public static bool Exists(string fabricPath, MCFabricInstaller fabricInstaller)
    {
        if (string.IsNullOrEmpty(fabricPath))
            return false;

        if (fabricInstaller == null)
            return false;

        if (string.IsNullOrEmpty(fabricInstaller.URL))
            return false;

        if (string.IsNullOrEmpty(fabricInstaller.Version))
            return false;

        return true;
    }
}
