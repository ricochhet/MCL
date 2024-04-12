using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Interfaces.Java;

public interface IQuiltLoaderDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltConfigUrls quiltConfigUrls
    );

    public static abstract Task<bool> DownloadQuiltIndex();
    public static abstract bool LoadQuiltIndex();
    public static abstract Task<bool> DownloadQuiltProfile();
    public static abstract bool LoadQuiltProfile();
    public static abstract bool LoadQuiltLoaderVersion();
    public static abstract Task<bool> DownloadQuiltLoader();
}
