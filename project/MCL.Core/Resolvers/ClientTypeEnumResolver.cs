using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class ClientTypeEnumResolver
{
    public static string ToString(ClientTypeEnum type) =>
        type switch
        {
            ClientTypeEnum.VANILLA => "net.minecraft.client.main.Main",
            ClientTypeEnum.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotClient",
            _ => throw new NotImplementedException(),
        };
}
