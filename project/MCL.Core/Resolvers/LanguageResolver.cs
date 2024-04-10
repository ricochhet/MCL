using System;
using MCL.Core.Enums.Services;

namespace MCL.Core.Resolvers;

public static class LanguageResolver
{
    public static string ToString(Language type) =>
        type switch
        {
            Language.ENGLISH => "en",
            Language.CHINESE => "cn",
            _ => throw new NotImplementedException(),
        };
}
