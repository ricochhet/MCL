using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces;

public interface IMCLibraryDownloader
{
    public static abstract Task<bool> Download(
        string minecraftPath,
        PlatformEnum minecraftPlatform,
        List<MCLibrary> libraries
    );
    public static abstract bool Exists(MCLibrary lib);
    public static abstract bool SkipLibrary(MCLibrary lib, PlatformEnum minecraftPlatform);
    public static abstract Task<bool> DownloadNatives(
        string minecraftPath,
        MCLibrary lib,
        PlatformEnum minecraftPlatform
    );
    public static abstract bool WindowsClassifierNativesExists(MCLibrary lib);
    public static abstract bool LinuxClassifierNativesExists(MCLibrary lib);
    public static abstract bool OSXClassifierNativesExists(MCLibrary lib);
}
