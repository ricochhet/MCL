using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public static class ServerTypeResolver
{
    public static string ToString(ServerType type) =>
        type switch
        {
            ServerType.VANILLA => "net.minecraft.server.main.Main",
            ServerType.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotServer",
            _ => throw new NotImplementedException(),
        };
}
