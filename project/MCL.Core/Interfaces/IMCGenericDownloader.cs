using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces;

public interface IMCGenericDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails);
    public static abstract bool Exists(MCVersionDetails versionDetails);
}
