using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeIndexDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls);
    public static abstract bool Exists(string minecraftPath, MCConfigUrls minecraftUrls);
}
