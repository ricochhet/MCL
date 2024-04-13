using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.MinecraftFabric;

public interface IFabricLoaderDownloader<T1, T2>
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T1 fabricProfile,
        T2 fabricConfigUrls
    );
}
