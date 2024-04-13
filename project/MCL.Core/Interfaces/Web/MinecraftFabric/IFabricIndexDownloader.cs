using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.MinecraftFabric;

public interface IFabricIndexDownloader<T>
{
    public static abstract Task<bool> Download(MCLauncherPath launcherPath, T fabricConfigUrls);
}
