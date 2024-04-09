using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricMappings
{
    [JsonPropertyName("gameVersion")]
    public string GameVersion { get; set; }

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

    public MCFabricMappings(string gameVersion, string separator, int build, string maven, string version, bool stable)
    {
        GameVersion = gameVersion;
        Separator = separator;
        Build = build;
        Maven = maven;
        Version = version;
        Stable = stable;
    }
}
