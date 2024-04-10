using System;
using MCL.Core.Enums;

namespace MCL.Core.Resolvers;

public static class ClientTypeResolver
{
    public static string ToString(ClientType type) =>
        type switch
        {
            ClientType.VANILLA => "net.minecraft.client.main.Main",
            ClientType.FABRIC => "net.fabricmc.loader.impl.launch.knot.KnotClient",
            _ => throw new NotImplementedException(),
        };
}
