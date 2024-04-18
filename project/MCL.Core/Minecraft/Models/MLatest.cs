using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLatest(string release, string snapshot)
{
    [JsonPropertyName("release")]
    public string Release { get; set; } = release;

    [JsonPropertyName("snapshot")]
    public string Snapshot { get; set; } = snapshot;
}
