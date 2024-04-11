using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Java;

public interface IFabricLoaderDownloadService
{
    public static abstract void Init(
        MCLauncherPath _launcherPath,
        MCLauncherVersion _launcherVersion,
        MCFabricConfigUrls _fabricConfigUrls
    );

    public static abstract Task<bool> DownloadFabricIndex();
    public static abstract bool LoadFabricIndex();
    public static abstract Task<bool> DownloadFabricProfile();
    public static abstract bool LoadFabricProfile();
    public static abstract bool LoadFabricLoaderVersion();
    public static abstract Task<bool> DownloadFabricLoader();
}
