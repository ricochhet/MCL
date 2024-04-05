using System.Text;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class VersionDetailsDownloader
{
    public static async Task<bool> Download(string minecraftPath, Version version)
    {
        if (version == null || string.IsNullOrEmpty(version?.URL))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version);
        string versionDetails = await Request.DoRequest(version.URL, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(versionDetails))
            return false;
        return true;
    }
}
