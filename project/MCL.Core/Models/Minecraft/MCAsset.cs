using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCAsset(string hash, int size)
{
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = hash;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;
}
