using System.Threading.Tasks;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces;

public interface IMCVersionDetailsDownloader
{
    public static abstract Task<bool> Download(string minecraftPath, MCVersion version);
    public static abstract bool Exists(string minecraftPath, MCVersion version);
}
