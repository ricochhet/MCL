using System.Text.Json.Serialization;

namespace MCL.Core.ModLoaders.Fabric.Models;

public class FabricLoader(string separator, int build, string maven, string version, bool stable)
{
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
