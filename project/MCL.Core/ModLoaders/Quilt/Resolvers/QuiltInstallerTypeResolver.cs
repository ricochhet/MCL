using System;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public static class QuiltInstallerTypeResolver
{
    public static string ToString(QuiltInstallerType type) =>
        type switch
        {
            QuiltInstallerType.CLIENT => "client",
            QuiltInstallerType.SERVER => "server",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
