using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Web.Minecraft;

public interface IMCVersionDetailsDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, MCVersion version);
}
