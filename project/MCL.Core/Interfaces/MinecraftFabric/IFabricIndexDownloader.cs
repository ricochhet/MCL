using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricIndexDownloader
{
    public static abstract Task<bool> Download(string fabricPath, MCFabricConfigUrls fabricUrls);
    public static abstract bool Exists(string fabricPath, MCFabricConfigUrls fabricUrls);
}
