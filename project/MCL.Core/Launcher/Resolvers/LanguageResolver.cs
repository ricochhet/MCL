using System;
using MCL.Core.Launcher.Enums;

namespace MCL.Core.Launcher.Resolvers;

public static class LanguageResolver
{
    public static string ToString(Language type) =>
        type switch
        {
            Language.ENGLISH => "en",
            Language.CHINESE => "cn",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
