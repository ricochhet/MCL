using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.MinecraftFabric;

public interface IFabricInstallerDownloadService<T>
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T fabricConfigUrls
    );

    public static abstract Task<bool> DownloadIndex();
    public static abstract bool LoadIndex();
    public static abstract bool LoadInstallerVersion();
    public static abstract Task<bool> DownloadInstaller();
}
