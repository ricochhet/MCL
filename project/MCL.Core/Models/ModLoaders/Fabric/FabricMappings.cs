using System.Text.Json.Serialization;

namespace MCL.Core.Models.ModLoaders.Fabric;

public class FabricMappings(string gameVersion, string separator, int build, string maven, string version, bool stable)
{
    [JsonPropertyName("gameVersion")]
    public string GameVersion { get; set; } = gameVersion;

    [JsonPropertyName("separator")]
    public string Separator { get; set; } = separator;

    [JsonPropertyName("build")]
    public int Build { get; set; } = build;

    [JsonPropertyName("maven")]
    public string Maven { get; set; } = maven;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;

    [JsonPropertyName("stable")]
    public bool Stable { get; set; } = stable;
}
