using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftQuilt;

public class MCQuiltLibrary(string name, string url)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

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
