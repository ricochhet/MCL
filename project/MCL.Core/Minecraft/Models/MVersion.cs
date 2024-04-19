using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MVersion(string id, string type, string url, string time, string releaseTime)
{
    [JsonPropertyName("id")]
    public string ID { get; set; } = id;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

    [JsonPropertyName("time")]
    public string Time { get; set; } = time;

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; } = releaseTime;
}