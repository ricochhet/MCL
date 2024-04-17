using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Services.Paper;

public interface IPaperServerDownloadService<T>
{
    public static abstract void Init(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion, T configUrls);

    public static abstract Task<bool> DownloadVersionManifest();
    public static abstract bool LoadVersionManifest(bool silent);
    public static abstract bool LoadServerVersion();
    public static abstract Task<bool> DownloadServer();
}
