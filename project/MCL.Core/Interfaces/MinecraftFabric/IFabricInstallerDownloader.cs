using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricInstallerDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCFabricInstaller fabricInstaller);
}
