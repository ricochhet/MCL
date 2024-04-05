using System.Collections.Generic;
using System.IO;
using MCL.Core.MiniCommon;
using MCL.Core.Providers;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftArgsResolver
{
    public const string MainClass = "net.minecraft.client.main.Main";

    public static string MainClassLibrary(string minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion, $"{minecraftVersion}.jar").Replace("\\", "/");
    }

    public static string Libraries(string minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion, $"{minecraftVersion}-natives").Replace("\\", "/");
    }

    public static int AssetIndexId(string minecraftPath)
    {
        List<string> fileName = FsProvider.GetFiles(
            Path.Combine(MinecraftPathResolver.AssetsPath(minecraftPath), "indexes"),
            "*.json",
            true
        );
        bool success = int.TryParse(fileName[0], out int id);
        if (success)
            return id;
        return -1;
    }
}
