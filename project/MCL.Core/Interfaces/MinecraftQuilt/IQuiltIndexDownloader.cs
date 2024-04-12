using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Interfaces.MinecraftQuilt;

public interface IQuiltIndexDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCQuiltConfigUrls quiltConfigUrls);
}
