using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Web.Minecraft;

public class FabricLoaderDownloaderErr
{
    public static bool Exists(MCFabricProfile fabricProfile, MCFabricConfigUrls fabricConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricLoaderJarUrl))
            return false;

        if (fabricProfile?.Libraries == null)
            return false;

        return true;
    }
}
