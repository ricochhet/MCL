using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCVersionDetailsDownloader
{
    public static abstract Task<bool> Download(MCLauncherPath minecraftPath, MCVersion version);
    public static abstract bool Exists(MCVersion version);
}
