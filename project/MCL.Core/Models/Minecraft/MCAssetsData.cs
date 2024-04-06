using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCAssetsData
{
    [JsonPropertyName("objects")]
    public Dictionary<string, MCAsset> Objects { get; set; }
}