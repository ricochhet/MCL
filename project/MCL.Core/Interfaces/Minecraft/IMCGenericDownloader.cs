using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCGenericDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails);
    public static abstract bool Exists(MCVersionDetails versionDetails);
}
