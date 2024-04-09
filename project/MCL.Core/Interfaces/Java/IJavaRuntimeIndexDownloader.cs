using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeIndexDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls);
}
