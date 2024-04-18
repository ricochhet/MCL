using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLatest(string release, string snapshot)
{
    [JsonPropertyName("release")]
    public string Release { get; set; } = release;

    [JsonPropertyName("snapshot")]
    public string Snapshot { get; set; } = snapshot;
}
