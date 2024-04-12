using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftQuilt;

public class MCQuiltLoader(string separator, int build, string maven, string version)
{
    [JsonPropertyName("separator")]
    public string Separator { get; set; } = separator;

    [JsonPropertyName("build")]
    public int Build { get; set; } = build;

    [JsonPropertyName("maven")]
    public string Maven { get; set; } = maven;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;
}
