using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces;

public interface IMCVersionManifestDownloader
{
    public static abstract Task<bool> Download(MCConfigUrls minecraftUrls, string minecraftPath);
    public static abstract bool Exists(MCConfigUrls minecraftUrls, string minecraftPath);
}
