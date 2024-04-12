using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftQuilt;

public class MCQuiltGame(string version, bool stable)
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = version;

    [JsonPropertyName("stable")]
    public bool Stable { get; set; } = stable;
}
