using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.Paper;

public interface IPaperIndexDownloader<T>
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T paperConfigUrls
    );
}
