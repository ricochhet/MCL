using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricIndexDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCFabricConfigUrls fabricConfigUrls);
    public static abstract bool Exists(MCFabricConfigUrls fabricConfigUrls);
}
