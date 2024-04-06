using System;

namespace MCL.Core.Resolvers;

public static class GenericEnumParser
{
    public static T Parse<T>(string value)
        where T : struct
    {
        if (Enum.TryParse(value, true, out T result))
            return result;
        throw new ArgumentException($"Invalid platform value: {value}");
    }
}
