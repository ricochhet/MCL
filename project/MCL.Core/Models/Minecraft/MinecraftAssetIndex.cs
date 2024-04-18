using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftAssetIndex(string id, string sha1, int size, int totalSize, string url)
{
    [JsonPropertyName("id")]
    public string ID { get; set; } = id;

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; } = sha1;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;

    [JsonPropertyName("totalSize")]
    public int TotalSize { get; set; } = totalSize;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;
}
