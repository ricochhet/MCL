using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class PlatformEnumResolver
{
    public static string Platform(PlatformEnum type) => type switch
    {
        PlatformEnum.WINDOWS => "windows",
        PlatformEnum.LINUX => "linux",
        PlatformEnum.OSX => "osx",
        _ => throw new NotImplementedException(),
    };
}