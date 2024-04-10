using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public static class RuleResolver
{
    public static string ToString(Rule type) =>
        type switch
        {
            Rule.ALLOW => "allow",
            Rule.DISALLOW => "disallow",
            _ => throw new NotImplementedException(),
        };
}
