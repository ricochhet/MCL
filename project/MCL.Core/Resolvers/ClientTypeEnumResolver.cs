using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class ClientTypeEnumResolver
{
    public static ClientTypeEnum Parse(string value)
    {
        if (Enum.TryParse(value, true, out ClientTypeEnum result))
            return result;
        throw new ArgumentException($"Invalid rule value: {value}");
    }

    public static string ToString(ClientTypeEnum type) =>
        type switch
        {
            ClientTypeEnum.VANILLA => "net.minecraft.client.main.Main",
            ClientTypeEnum.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotClient",
            _ => throw new NotImplementedException(),
        };
}
