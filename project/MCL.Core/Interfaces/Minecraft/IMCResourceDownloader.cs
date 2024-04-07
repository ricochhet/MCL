using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCResourceDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath minecraftPath,
        MCConfigUrls minecraftUrls,
        MCAssetsData assets
    );
    public static abstract bool Exists(MCConfigUrls minecraftUrls, MCAssetsData assets);
}
