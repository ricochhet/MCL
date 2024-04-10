using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Minecraft;

public interface IMCLibraryDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        Platform platform,
        List<MCLibrary> libraries
    );
    public static abstract bool SkipLibrary(MCLibrary lib, Platform platform);
    public static abstract Task<bool> DownloadNatives(MCLauncherPath launcherPath, MCLibrary lib, Platform platform);
}
