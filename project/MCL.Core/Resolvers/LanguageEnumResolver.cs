using System;
using MCL.Core.Enums.Services;

namespace MCL.Core.Resolvers;

public class LanguageEnumResolver
{
    public static string ToString(LanguageEnum type) =>
        type switch
        {
            LanguageEnum.ENGLISH => "en",
            LanguageEnum.CHINESE => "cn",
            _ => throw new NotImplementedException(),
        };
}
