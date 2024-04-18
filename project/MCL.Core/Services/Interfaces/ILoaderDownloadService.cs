using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Services.Interfaces;

public interface ILoaderDownloadService<T>
{
    public static abstract void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, T minecraftUrls);

    public static abstract Task<bool> DownloadIndex();
    public static abstract bool LoadIndex();
    public static abstract Task<bool> DownloadProfile();
    public static abstract bool LoadProfile();
    public static abstract bool LoadLoaderVersion();
    public static abstract Task<bool> DownloadLoader();
}
