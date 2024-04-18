using System;
using MCL.Core.Enums.MinecraftQuilt;

namespace MCL.Core.Resolvers.ModLoaders.Quilt;

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
