using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCGenericDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails);
    public static abstract bool Exists(string minecraftPath, MCVersionDetails versionDetails);
}
