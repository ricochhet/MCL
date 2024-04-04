using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class RuleEnumResolver
{
    public static RuleEnum Parse(string value)
    {
        if (Enum.TryParse(value, true, out RuleEnum result))
            return result;
        throw new ArgumentException($"Invalid rule value: {value}");
    }

    public static string ToString(RuleEnum type) => type switch
    {
        RuleEnum.ALLOW => "allow",
        RuleEnum.DISALLOW => "disallow",
        _ => throw new NotImplementedException(),
    };
}