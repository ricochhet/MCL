using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.MinecraftFabric;

public interface IFabricProfileDownloader<T>
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T fabricConfigUrls
    );
}
