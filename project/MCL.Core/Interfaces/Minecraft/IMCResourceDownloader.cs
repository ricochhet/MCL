using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCResourceDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets);
    public static abstract bool Exists(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets);
}
