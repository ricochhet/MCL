using System.Collections.Generic;

namespace MCL.Core.Models.Minecraft;

public class MinecraftArgs
{
    public string InitialHeapSize { get; set; } = string.Empty;
    public string MaxHeapSize { get; set; } = string.Empty;
    public string ClassPath { get; set; } = string.Empty;
    public string MainClass { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string GameDir { get; set; } = string.Empty;
    public string AssetIndex { get; set; } = string.Empty;
    public string AssetsDir { get; set; } = string.Empty;
    public string Uuid { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Xuid { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string VersionType { get; set; } = string.Empty;
    public List<string> AdditionalArguments { get; set; } = [];
}
