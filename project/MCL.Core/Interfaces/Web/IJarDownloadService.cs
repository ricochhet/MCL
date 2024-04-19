using System.Threading.Tasks;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Interfaces.Web;

public interface IJarDownloadService<T>
{
    public static abstract void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, T mUrls);

    public static abstract Task<bool> DownloadVersionManifest();
    public static abstract bool LoadVersionManifest();
    public static abstract bool LoadVersionManifestWithoutLogging();
    public static abstract bool LoadVersion();
    public static abstract Task<bool> DownloadJar();
}
