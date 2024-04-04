using MCL.Core.Models;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Config.Minecraft;

public static class MinecraftArgGenerator
{
    public static string Generate(MinecraftArgs minecraftArgs)
    {
        return $"-Xms{minecraftArgs.InitialHeapSize}m "
            + $"-Xmx{minecraftArgs.MaxHeapSize}m "
            + $"{string.Join(" ", minecraftArgs.AdditionalArguments)} "
            + $"-cp {minecraftArgs.ClassPath} {minecraftArgs.MainClass} "
            + $"--username {minecraftArgs.Username} "
            + $"--userType {minecraftArgs.UserType} "
            + $"--gameDir {minecraftArgs.GameDir} "
            + $"--assetIndex {minecraftArgs.AssetIndex} "
            + $"--assetsDir {minecraftArgs.AssetsDir} "
            + $"--accessToken {minecraftArgs.AccessToken} "
            + $"--uuid {minecraftArgs.Uuid} "
            + $"--clientId {minecraftArgs.ClientId} "
            + $"--xuid {minecraftArgs.Xuid} "
            + $"--version {minecraftArgs.Version} "
            + $"--versionType {minecraftArgs.VersionType}";
    }
}
