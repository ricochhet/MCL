using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class VersionDetailsDownloader
{
    public static async Task<bool> Download(string minecraftPath, Version version)
    {
        return await Request.Download(
            version.URL,
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );
    }
}