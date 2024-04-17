using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.MinecraftFabric;
using MCL.Core.Services.MinecraftQuilt;
using MCL.Core.Services.Paper;

namespace MCL.Core.Services.Minecraft;

public class VersionManagerService : IDownloadService
{
    private static MCLauncherPath LauncherPath;
    private static bool Loaded = false;

    public static void Init(MCLauncherPath launcherPath)
    {
        LauncherPath = launcherPath;
        Loaded = true;
    }

    public static async Task<bool> Setup()
    {
        if (!Loaded)
            return false;

        if (ValidVersions())
            return true;

        if (!await Download())
            return false;

        return true;
    }

    public static async Task<bool> Download()
    {
        if (!Loaded)
            return false;

        if (!await MinecraftDownloadService.DownloadVersionManifest())
            return false;

        if (MinecraftDownloadService.LoadVersion() && !await MinecraftDownloadService.DownloadVersionDetails())
            return false;

        if (!await FabricInstallerDownloadService.DownloadIndex())
            return false;

        if (!await QuiltInstallerDownloadService.DownloadIndex())
            return false;

        if (!await PaperServerDownloadService.DownloadIndex())
            return false;

        return true;
    }

    public static bool ValidVersions()
    {
        if (!Loaded)
            return false;

        MCVersionManifest versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(LauncherPath)
        );

        MCFabricIndex fabricIndex = Json.Load<MCFabricIndex>(FabricPathResolver.DownloadedIndexPath(LauncherPath));
        MCQuiltIndex quiltIndex = Json.Load<MCQuiltIndex>(QuiltPathResolver.DownloadedIndexPath(LauncherPath));

        return versionManifest != null && fabricIndex != null && quiltIndex != null;
    }
}
