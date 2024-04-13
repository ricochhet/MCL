using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Web.Minecraft;

public interface IMCLibraryDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        Platform platform,
        MCVersionDetails versionDetails
    );
    public static abstract bool SkipLibrary(MCLibrary lib, Platform platform);
    public static abstract Task<bool> DownloadNatives(MCLauncherPath launcherPath, MCLibrary lib, Platform platform);
}