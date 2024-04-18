using System;
using MCL.Core.Enums.Java;

namespace MCL.Core.Resolvers.Java;

public static class JavaRuntimePlatformResolver
{
    public static string ToString(JavaRuntimePlatform type) =>
        type switch
        {
            JavaRuntimePlatform.GAMECORE => "gamecore",
            JavaRuntimePlatform.LINUX => "linux",
            JavaRuntimePlatform.LINUXI386 => "linux-i386",
            JavaRuntimePlatform.MACOS => "mac-os",
            JavaRuntimePlatform.MACOSARM64 => "mac-os-arm64",
            JavaRuntimePlatform.WINDOWSARM64 => "windows-arm64",
            JavaRuntimePlatform.WINDOWSX64 => "windows-x64",
            JavaRuntimePlatform.WINDOWSX86 => "windows-x86",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };

    public static string ToPlatformString(JavaRuntimePlatform type) =>
        type switch
        {
            JavaRuntimePlatform.WINDOWSARM64 => "windows",
            JavaRuntimePlatform.WINDOWSX64 => "windows",
            JavaRuntimePlatform.WINDOWSX86 => "windows",
            JavaRuntimePlatform.LINUX => "linux",
            JavaRuntimePlatform.LINUXI386 => "linux",
            JavaRuntimePlatform.MACOS => "osx",
            JavaRuntimePlatform.MACOSARM64 => "osx",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
