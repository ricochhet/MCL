using System.Threading.Tasks;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Interfaces.Web;

public interface ILoaderDownloadService<T>
{
    public static abstract void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, T mUrls);

    public static abstract Task<bool> DownloadVersionManifest();
    public static abstract bool LoadVersionManifest();
    public static abstract Task<bool> DownloadProfile();
    public static abstract bool LoadProfile();
    public static abstract bool LoadLoaderVersion();
    public static abstract Task<bool> DownloadLoader();
}
