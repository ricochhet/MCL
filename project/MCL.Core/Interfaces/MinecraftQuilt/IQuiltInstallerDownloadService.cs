using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Interfaces.MinecraftQuilt;

public interface IQuiltInstallerDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltConfigUrls quiltConfigUrls
    );

    public static abstract Task<bool> DownloadQuiltIndex();
    public static abstract bool LoadQuiltIndex();
    public static abstract bool LoadQuiltInstallerVersion();
    public static abstract Task<bool> DownloadQuiltInstaller();
}
