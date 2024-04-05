using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLatest
{
    [JsonPropertyName("release")]
    public string Release { get; set; }

    [JsonPropertyName("snapshot")]
    public string Snapshot { get; set; }
}
