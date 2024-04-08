using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ClassPathHelper
{
    public static string CreateClassPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return default;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return default;

        string separator = Environment.OSVersion.Platform switch
        {
            PlatformID.Unix or PlatformID.MacOSX => ":",
            PlatformID.Win32NT => ";",
            _ => throw new Exception("Unsupported OS."),
        };
        string libPath = Path.Combine(launcherPath.MCPath + "/", "libraries");
        string[] libraries = VFS.GetFiles(libPath, "*");
        libraries = libraries.Prepend(MinecraftPathResolver.ClientLibrary(launcherVersion)).ToArray();

        return string.Join(
            separator,
            libraries.Select(lib => lib.Replace(launcherPath.MCPath + "/", "").Replace("\\", "/"))
        );
    }
}
