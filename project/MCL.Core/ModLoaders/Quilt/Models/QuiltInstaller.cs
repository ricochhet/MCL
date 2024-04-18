using System.Text.Json.Serialization;

namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltInstaller(string url, string maven, string version)
{
    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

    [JsonPropertyName("maven")]
    public string Maven { get; set; } = maven;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;
}
