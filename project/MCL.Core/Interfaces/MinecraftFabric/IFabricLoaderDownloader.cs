using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricLoaderDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath fabricPath, MCFabricInstaller fabricInstaller);
    public static abstract bool Exists(MCFabricInstaller fabricInstaller);
}
