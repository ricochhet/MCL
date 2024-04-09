using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricLoader
{
    [JsonPropertyName("separator")]
    public string Separator { get; set; }

    [JsonPropertyName("build")]
    public int Build { get; set; }

    [JsonPropertyName("maven")]
    public string Maven { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("stable")]
    public bool Stable { get; set; }

    public MCFabricLoader(string separator, int build, string maven, string version, bool stable)
    {
        Separator = separator;
        Build = build;
        Maven = maven;
        Version = version;
        Stable = stable;
    }
}
