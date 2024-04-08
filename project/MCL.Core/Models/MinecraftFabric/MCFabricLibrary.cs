using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricLibrary
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string URL { get; set; }

    [JsonPropertyName("md5")]
    public string MD5 { get; set; }

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; }

    [JsonPropertyName("sha256")]
    public string SHA256 { get; set; }

    [JsonPropertyName("sha512")]
    public string SHA512 { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    public static string ParseURL(string name, string url)
    {
        string path;
        string[] parts = name.Split(":", 3);
        path = parts[0].Replace(".", "/") + "/" + parts[1] + "/" + parts[2] + "/" + parts[1] + "-" + parts[2] + ".jar";
        return url + path;
    }

    public static string ParsePath(string name)
    {
        string[] parts = name.Split(":", 3);
        char separator = '/';
        string path =
            parts[0].Replace('.', separator)
            + separator
            + parts[1]
            + separator
            + parts[2]
            + separator
            + parts[1]
            + "-"
            + parts[2]
            + ".jar";
        return path.Replace(" ", "_");
    }
}
