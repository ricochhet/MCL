using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionDetailsDownloader : IMCVersionDetailsDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersion version)
    {
        if (!Exists(minecraftPath, version))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version);
        string versionDetails = await Request.DoRequest(version.URL, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(versionDetails))
            return false;
        return true;
    }

    public static bool Exists(string minecraftPath, MCVersion version)
    {
        if (version == null)
            return false;

        if (string.IsNullOrEmpty(version.URL))
            return false;

        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        return true;
    }
}
