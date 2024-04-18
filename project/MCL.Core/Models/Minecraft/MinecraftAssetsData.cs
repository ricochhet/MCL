using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftAssetsData
{
    [JsonPropertyName("objects")]
    public Dictionary<string, MinecraftAsset> Objects { get; set; }

    public MinecraftAssetsData() { }
}
