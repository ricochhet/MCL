using System;
using MCL.Core.Enums.MinecraftFabric;

namespace MCL.Core.Resolvers.ModLoaders.Fabric;

public static class FabricInstallerTypeResolver
{
    public static string ToString(FabricInstallerType type) =>
        type switch
        {
            FabricInstallerType.CLIENT => "client",
            FabricInstallerType.SERVER => "server",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
