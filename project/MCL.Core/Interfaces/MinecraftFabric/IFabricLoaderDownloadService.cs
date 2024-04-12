using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.MinecraftFabric;

public interface IFabricLoaderDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    );

    public static abstract Task<bool> DownloadFabricIndex();
    public static abstract bool LoadFabricIndex();
    public static abstract Task<bool> DownloadFabricProfile();
    public static abstract bool LoadFabricProfile();
    public static abstract bool LoadFabricLoaderVersion();
    public static abstract Task<bool> DownloadFabricLoader();
}
