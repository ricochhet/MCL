using System;

namespace MCL.Core.Launcher.Resolvers;

public static class GenericEnumParser
{
    public static T Parse<T>(string value, T fallback)
        where T : struct
    {
        if (Enum.TryParse(value.Replace("-", "_"), true, out T result))
            return result;
        return fallback;
    }
}
