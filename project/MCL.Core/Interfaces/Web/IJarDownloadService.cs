using System.Threading.Tasks;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Interfaces.Web;

public interface IJarDownloadService<T>
{
    public static abstract void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, T minecraftUrls);

    public static abstract Task<bool> DownloadIndex();
    public static abstract bool LoadIndex();
    public static abstract bool LoadVersion();
    public static abstract Task<bool> DownloadJar();
}
