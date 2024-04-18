using System;
using MCL.Core.ModLoaders.Fabric.Enums;

namespace MCL.Core.ModLoaders.Fabric.Resolvers;

public static class FabricInstallerTypeResolver
{
    public static string ToString(FabricInstallerType type) =>
        type switch
        {
            FabricInstallerType.INSTALL_CLIENT => "client",
            FabricInstallerType.INSTALL_SERVER => "server",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
