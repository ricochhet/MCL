using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.MinecraftFabric;

public interface IFabricInstallerDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    );

    public static abstract Task<bool> DownloadFabricIndex();
    public static abstract bool LoadFabricIndex();
    public static abstract bool LoadFabricInstallerVersion();
    public static abstract Task<bool> DownloadFabricInstaller();
}
