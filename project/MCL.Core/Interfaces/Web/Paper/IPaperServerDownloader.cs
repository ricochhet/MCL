using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.Paper;

public interface IPaperServerDownloader<T1, T2>
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T1 paperVersionManifest,
        T2 paperConfigUrls
    );
}
