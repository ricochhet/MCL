using System;
using System.Data;
using MCL.Core.Minecraft.Enums;

namespace MCL.Core.Minecraft.Resolvers;

public static class RuleTypeResolver
{
    public static string ToString(RuleType type) =>
        type switch
        {
            RuleType.ALLOW => "allow",
            RuleType.DISALLOW => "disallow",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
