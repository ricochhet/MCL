using System;
using System.Linq;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ClassPathHelper
{
    public static string CreateClassPath(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        JavaRuntimePlatform platform
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return default;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return default;

        string separator = platform switch
        {
            JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386
            or JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64
                => ":",
            JavaRuntimePlatform.WINDOWSX64 or JavaRuntimePlatform.WINDOWSX86 or JavaRuntimePlatform.WINDOWSARM64 => ";",
            _ => throw new NotImplementedException("Unsupported OS."),
        };
        string libPath = VFS.FromCwd(launcherPath.Path, "libraries");
        string[] libraries = VFS.GetFiles(libPath, "*");
        libraries = libraries.Prepend(MinecraftPathResolver.ClientLibrary(launcherVersion)).ToArray();

        return string.Join(
            separator,
            libraries.Select(lib => lib.Replace("\\", "/").Replace(launcherPath.Path + "/", string.Empty))
        );
    }
}
