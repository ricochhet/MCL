using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricIntermediary
{
    [JsonPropertyName("maven")]
    public string Maven { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("stable")]
    public bool Stable { get; set; }

    public MCFabricIntermediary(string maven, string version, bool stable)
    {
        Maven = maven;
        Version = version;
        Stable = stable;
    }
}
