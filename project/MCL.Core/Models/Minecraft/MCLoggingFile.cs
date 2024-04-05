using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLoggingFile
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("url")]
    public string URL { get; set; }
}
