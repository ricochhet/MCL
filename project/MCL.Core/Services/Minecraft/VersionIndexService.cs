using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Services.MinecraftFabric;
using MCL.Core.Services.MinecraftQuilt;

namespace MCL.Core.Services.Minecraft;

public class VersionIndexService : IDownloadService
{
    public static async Task<bool> Download()
    {
        if (!await MinecraftDownloadService.DownloadVersionManifest())
            return false;

        if (!await FabricInstallerDownloadService.DownloadIndex())
            return false;

        if (!await QuiltInstallerDownloadService.DownloadIndex())
            return false;

        return true;
    }
}
