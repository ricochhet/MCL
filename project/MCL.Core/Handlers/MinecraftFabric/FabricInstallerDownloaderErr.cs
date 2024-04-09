using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Web.Minecraft;

public class FabricInstallerDownloaderErr
{
    public static bool Exists(MCFabricInstaller fabricInstaller)
    {
        if (string.IsNullOrWhiteSpace(fabricInstaller?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(fabricInstaller?.Version))
            return false;

        return true;
    }
}
