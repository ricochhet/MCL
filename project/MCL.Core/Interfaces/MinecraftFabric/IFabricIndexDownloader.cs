using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.MinecraftFabric;

public interface IFabricIndexDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCFabricConfigUrls fabricConfigUrls);
}
