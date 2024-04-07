using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Minecraft;

public interface IFabricLoaderDownloader
{
    public static abstract Task<bool> Download(string fabricPath, MCFabricInstaller fabricInstaller);
    public static abstract bool Exists(string fabricPath, MCFabricInstaller fabricInstaller);
}
