using System;
using System.Linq;
using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ClassPathHelper
{
    public static string CreateClassPath(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        JavaRuntimePlatform platform
    )
    {
        if (!launcherVersion.VersionExists())
            return string.Empty;

        string separator = platform switch
        {
            JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386
            or JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64
                => ":",
            JavaRuntimePlatform.WINDOWSX64 or JavaRuntimePlatform.WINDOWSX86 or JavaRuntimePlatform.WINDOWSARM64 => ";",
            _ => throw new ArgumentOutOfRangeException(nameof(platform), "Unsupported OS."),
        };
        string libPath = VFS.Combine(launcherPath.Path, "libraries");
        string[] libraries = VFS.GetFiles(libPath, "*");
        libraries = libraries.Prepend(MPathResolver.ClientLibrary(launcherVersion)).ToArray();

        return string.Join(
            separator,
            libraries.Select(lib => lib.Replace("\\", "/").Replace(launcherPath.Path + "/", string.Empty))
        );
    }
}
