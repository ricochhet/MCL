using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricProfileDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath fabricInstallerPath,
        MCLauncherVersion minecraftVersion,
        MCFabricConfigUrls fabricUrls
    );
    public static abstract bool Exists(MCFabricConfigUrls fabricUrls);
}
