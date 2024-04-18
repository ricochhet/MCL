using System;
using MCL.Core.Launcher.Enums;

namespace MCL.Core.Launcher.Resolvers;

public static class ClientTypeResolver
{
    public static string ToString(ClientType type) =>
        type switch
        {
            ClientType.VANILLA => "net.minecraft.client.main.Main",
            ClientType.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotClient",
            ClientType.QUILT => "org.quiltmc.loader.impl.launch.knot.KnotClient",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
