using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Interfaces.Java;

public interface IFabricInstallerDownloadService
{
    public static abstract void Init(
        MCLauncherPath _launcherPath,
        MCLauncherVersion _launcherVersion,
        MCFabricConfigUrls _fabricConfigUrls
    );

    public static abstract Task<bool> DownloadFabricIndex();
    public static abstract bool LoadFabricIndex();
    public static abstract bool LoadFabricInstallerVersion();
    public static abstract Task<bool> DownloadFabricInstaller();
}
