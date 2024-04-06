using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class RuleEnumResolver
{
    public static string ToString(RuleEnum type) =>
        type switch
        {
            RuleEnum.ALLOW => "allow",
            RuleEnum.DISALLOW => "disallow",
            _ => throw new NotImplementedException(),
        };
}
