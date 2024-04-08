using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class FabricProfileDownloader : IFabricProfileDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath minecraftPath,
        MCLauncherVersion minecraftVersion,
        MCFabricConfigUrls fabricUrls
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!MCLauncherVersion.Exists(minecraftVersion))
            return false;

        if (!Exists(fabricUrls))
            return false;

        string fabricProfile = await Request.DoRequest(
            MinecraftFabricPathResolver.FabricLoaderProfileUrlPath(fabricUrls, minecraftVersion),
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(minecraftPath, minecraftVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrEmpty(fabricProfile))
            return false;
        return true;
    }

    public static bool Exists(MCFabricConfigUrls fabricUrls)
    {
        if (fabricUrls == null)
            return false;

        if (string.IsNullOrEmpty(fabricUrls.FabricLoaderProfileUrl))
            return false;

        return true;
    }
}
