using System.Text.Json.Serialization;

namespace MCL.Core.Models.ModLoaders.Quilt;

public class QuiltHashed(string maven, string version, bool stable)
{
    [JsonPropertyName("maven")]
    public string Maven { get; set; } = maven;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;

    [JsonPropertyName("stable")]
    public bool Stable { get; set; } = stable;
}
