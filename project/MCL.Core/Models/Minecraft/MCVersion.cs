using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCVersion
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("url")]
    public string URL { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; }
}
