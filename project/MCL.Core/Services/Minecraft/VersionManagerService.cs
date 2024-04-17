using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Paper;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Resolvers.Paper;
using MCL.Core.Services.MinecraftFabric;
using MCL.Core.Services.MinecraftQuilt;
using MCL.Core.Services.Paper;

namespace MCL.Core.Services.Minecraft;

public class VersionManagerService : IDownloadService
{
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static bool Loaded = false;

    private static MCVersionManifest versionManifest;
    private static MCFabricIndex fabricIndex;
    private static MCQuiltIndex quiltIndex;
    private static PaperVersionManifest paperVersionManifest;

    public static void Init(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        Loaded = true;
    }

    public static async Task<bool> Setup()
    {
        if (!Loaded)
            return false;

        if (VersionManifestsExists())
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

    public static bool VersionManifestsExists()
    {
        if (!Loaded)
            return false;

        versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(LauncherPath)
        );

        fabricIndex = Json.Load<MCFabricIndex>(FabricPathResolver.DownloadedIndexPath(LauncherPath));
        quiltIndex = Json.Load<MCQuiltIndex>(QuiltPathResolver.DownloadedIndexPath(LauncherPath));
        paperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.DownloadedIndexPath(LauncherPath, LauncherVersion)
        );

        return versionManifest != null && fabricIndex != null && quiltIndex != null && paperVersionManifest != null;
    }
}
