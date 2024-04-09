using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCAsset
{
    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    public MCAsset(string hash, int size)
    {
        Hash = hash;
        Size = size;
    }
}
