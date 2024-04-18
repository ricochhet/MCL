using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Services.Minecraft;

public interface IMinecraftDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCLauncherSettings launcherSettings,
        MCConfigUrls configUrls
    );

    public static abstract Task<bool> DownloadVersionManifest();
    public static abstract bool LoadVersionManifest();
    public static abstract bool LoadVersion();
    public static abstract Task<bool> DownloadVersionDetails();
    public static abstract bool LoadVersionDetails();
    public static abstract Task<bool> DownloadLibraries();
    public static abstract Task<bool> DownloadClient();
    public static abstract Task<bool> DownloadClientMappings();
    public static abstract Task<bool> DownloadServer();
    public static abstract Task<bool> DownloadServerMappings();
    public static abstract Task<bool> DownloadAssetIndex();
    public static abstract bool LoadAssetIndex();
    public static abstract Task<bool> DownloadResources();
    public static abstract Task<bool> DownloadLogging();
}
