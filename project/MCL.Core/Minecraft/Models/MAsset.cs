using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MAsset(string hash, int size)
{
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = hash;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;
}
