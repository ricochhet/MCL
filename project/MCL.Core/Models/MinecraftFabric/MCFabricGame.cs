using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricGame
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("stable")]
    public bool Stable { get; set; }

    public MCFabricGame(string version, bool stable)
    {
        Version = version;
        Stable = stable;
    }
}
