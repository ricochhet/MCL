using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeManifest
{
    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
