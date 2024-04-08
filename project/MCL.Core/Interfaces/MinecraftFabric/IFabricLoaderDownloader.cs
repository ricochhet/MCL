using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricLoaderDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricProfile fabricProfile,
        MCFabricConfigUrls fabricConfigUrls
    );
    public static abstract bool Exists(MCFabricProfile fabricProfile, MCFabricConfigUrls fabricConfigUrls);
}
