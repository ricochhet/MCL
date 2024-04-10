using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public static class PlatformResolver
{
    public static string ToString(Platform type) =>
        type switch
        {
            Platform.WINDOWS => "windows",
            Platform.LINUX => "linux",
            Platform.OSX => "osx",
            _ => throw new NotImplementedException(),
        };
}
