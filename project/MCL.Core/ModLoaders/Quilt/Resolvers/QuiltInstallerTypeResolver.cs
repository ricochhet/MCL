using System;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public static class QuiltInstallerTypeResolver
{
    public static string ToString(QuiltInstallerType type) =>
        type switch
        {
            QuiltInstallerType.INSTALL_CLIENT => "client",
            QuiltInstallerType.INSTALL_SERVER => "server",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
