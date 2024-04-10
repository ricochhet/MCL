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
            _ => throw new NotImplementedException(),
        };
}
