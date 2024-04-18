using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MAssetsData
{
    [JsonPropertyName("objects")]
    public Dictionary<string, MAsset> Objects { get; set; }

    public MAssetsData() { }
}
