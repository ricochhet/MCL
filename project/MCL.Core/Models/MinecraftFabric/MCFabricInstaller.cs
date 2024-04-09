using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricInstaller
{
    [JsonPropertyName("url")]
    public string URL { get; set; }

    [JsonPropertyName("maven")]
    public string Maven { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("stable")]
    public bool Stable { get; set; }

    public MCFabricInstaller(string url, string maven, string version, bool stable)
    {
        URL = url;
        Maven = maven;
        Version = version;
        Stable = stable;
    }
}
