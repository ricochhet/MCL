using System;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Launcher.Resolvers;

public static class ClientTypeResolver
{
    public static string ToString(ClientType type, MainClassNames mainClassNames) =>
        type switch
        {
            ClientType.VANILLA => mainClassNames.Vanilla,
            ClientType.FABRIC => mainClassNames.Fabric,
            ClientType.QUILT => mainClassNames.Quilt,
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
