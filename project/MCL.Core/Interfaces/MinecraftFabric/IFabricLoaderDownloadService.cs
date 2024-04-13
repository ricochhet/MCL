using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.MinecraftFabric;

public interface IFabricLoaderDownloadService<T>
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T fabricConfigUrls
    );

    public static abstract Task<bool> DownloadIndex();
    public static abstract bool LoadIndex();
    public static abstract Task<bool> DownloadProfile();
    public static abstract bool LoadProfile();
    public static abstract bool LoadLoaderVersion();
    public static abstract Task<bool> DownloadLoader();
}
