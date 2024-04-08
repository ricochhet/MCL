using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCVersionManifestDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls);
    public static abstract bool Exists(MCConfigUrls configUrls);
}
