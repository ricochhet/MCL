using System;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;

namespace MCL.Core.Resolvers.Java;

public class JavaRuntimePlatformEnumResolver
{
    public static string ToString(JavaRuntimePlatformEnum type) =>
        type switch
        {
            JavaRuntimePlatformEnum.GAMECORE => "gamecore",
            JavaRuntimePlatformEnum.LINUX => "linux",
            JavaRuntimePlatformEnum.LINUXI386 => "linux-i386",
            JavaRuntimePlatformEnum.MACOS => "mac-os",
            JavaRuntimePlatformEnum.MACOSARM64 => "mac-os-arm64",
            JavaRuntimePlatformEnum.WINDOWSARM64 => "windows-arm64",
            JavaRuntimePlatformEnum.WINDOWSX64 => "windows-x64",
            JavaRuntimePlatformEnum.WINDOWSX86 => "windows-x86",
            _ => throw new NotImplementedException(),
        };
}
