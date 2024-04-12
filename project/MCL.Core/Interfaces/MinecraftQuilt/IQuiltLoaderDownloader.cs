using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Interfaces.Minecraft;

public interface IQuiltLoaderDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltProfile quiltProfile,
        MCQuiltConfigUrls quiltConfigUrls
    );
}
