using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCAssetIndex
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalSize")]
    public int TotalSize { get; set; }

    [JsonPropertyName("url")]
    public string URL { get; set; }
}
