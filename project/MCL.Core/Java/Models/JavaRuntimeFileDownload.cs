using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntimeFileDownload(string sha1, int size, string url)
{
    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; } = sha1;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;
}
