using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCArtifact
{
    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("url")]
    public string URL { get; set; }

    public MCArtifact(string path, string sha1, int size, string url)
    {
        Path = path;
        SHA1 = sha1;
        Size = size;
        URL = url;
    }
}
