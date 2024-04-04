using System.Collections.Generic;

namespace MCL.Core.Models.Minecraft;

public class MinecraftArgs
{
    public string InitialHeapSize { get; set; }
    public string MaxHeapSize { get; set; }
    public string ClassPath { get; set; }
    public string MainClass { get; set; }
    public string Username { get; set; }
    public string UserType { get; set; }
    public string GameDir { get; set; }
    public string AssetIndex { get; set; }
    public string AssetsDir { get; set; }
    public string Uuid { get; set; }
    public string ClientId { get; set; }
    public string Xuid { get; set; }
    public string AccessToken { get; set; }
    public string Version { get; set; }
    public string VersionType { get; set; }
    public List<string> AdditionalArguments { get; set; }
}
