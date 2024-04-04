using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class PlatformEnumResolver
{
    public static PlatformEnum Parse(string value)
    {
        if (Enum.TryParse(value, true, out PlatformEnum result))
            return result;
        throw new ArgumentException($"Invalid platform value: {value}");
    }

    public static string ToString(PlatformEnum type) =>
        type switch
        {
            PlatformEnum.WINDOWS => "windows",
            PlatformEnum.LINUX => "linux",
            PlatformEnum.OSX => "osx",
            _ => throw new NotImplementedException(),
        };
}
