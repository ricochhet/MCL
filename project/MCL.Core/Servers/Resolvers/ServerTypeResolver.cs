using System;
using MCL.Core.Servers.Enums;

namespace MCL.Core.Servers.Resolvers;

public static class ServerTypeResolver
{
    public static string ToString(ServerType type) =>
        type switch
        {
            ServerType.VANILLA => "net.minecraft.server.main.Main",
            ServerType.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotServer",
            ServerType.QUILT => "org.quiltmc.loader.impl.launch.knot.KnotServer",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
