using System.Collections.Generic;
using System.IO;

namespace MCL.Core.MiniCommon;

public static class MinecraftArgs
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
            Path.Combine(MinecraftPath.AssetsPath(minecraftPath), "indexes"),
            "*.json",
            true
        );
        bool success = int.TryParse(fileName[0], out int id);
        if (success)
            return id;
        return -1;
    }
}
