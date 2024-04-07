using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces;

public interface IMCResourceDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets);
    public static abstract bool Exists(MCConfigUrls minecraftUrls, MCAssetsData assets);
}
