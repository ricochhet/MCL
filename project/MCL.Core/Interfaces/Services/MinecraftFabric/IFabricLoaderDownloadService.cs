using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Services.MinecraftFabric;

public interface IFabricLoaderDownloadService<T>
{
    public static abstract void Init(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion, T configUrls);

    public static abstract Task<bool> DownloadIndex();
    public static abstract bool LoadIndex();
    public static abstract Task<bool> DownloadProfile();
    public static abstract bool LoadProfile();
    public static abstract bool LoadLoaderVersion();
    public static abstract Task<bool> DownloadLoader();
}
