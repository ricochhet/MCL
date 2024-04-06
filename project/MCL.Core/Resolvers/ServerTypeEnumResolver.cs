using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public class ServerTypeEnumResolver
{
    public static string ToString(ServerTypeEnum type) =>
        type switch
        {
            ServerTypeEnum.VANILLA => "net.minecraft.server.main.Main",
            ServerTypeEnum.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotServer",
            _ => throw new NotImplementedException(),
        };
}
