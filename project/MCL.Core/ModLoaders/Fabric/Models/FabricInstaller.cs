using System.Text.Json.Serialization;

namespace MCL.Core.ModLoaders.Fabric.Models;

public class FabricInstaller(string url, string maven, string version, bool stable)
{
    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

    [JsonPropertyName("maven")]
    public string Maven { get; set; } = maven;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;

    [JsonPropertyName("stable")]
    public bool Stable { get; set; } = stable;
}
