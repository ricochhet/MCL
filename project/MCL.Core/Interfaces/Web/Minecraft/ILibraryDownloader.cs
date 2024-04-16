using System.Threading.Tasks;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Web.Minecraft;

public interface ILibraryDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherSettings launcherSettings,
        MCVersionDetails versionDetails
    );
    public static abstract bool SkipLibrary(MCLibrary lib, MCLauncherSettings launcherSettings);
    public static abstract Task<bool> DownloadNatives(
        MCLauncherPath launcherPath,
        MCLibrary lib,
        MCLauncherSettings launcherSettings
    );
}
