using System.Text.Json.Serialization;

namespace MCL.Core.Models.ModLoaders.Quilt;

public class QuiltMappings(string gameVersion, string separator, int build, string maven, string version, string hashed)
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

    [JsonPropertyName("hashed")]
    public string Hashed { get; set; } = hashed;
}
