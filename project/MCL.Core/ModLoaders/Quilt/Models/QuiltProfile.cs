using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCL.Core.Minecraft.Models;

namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltProfile
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("inheritsFrom")]
    public string InheritsFrom { get; set; }

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("mainClass")]
    public string MainClass { get; set; }

    [JsonPropertyName("arguments")]
    public MArgument Arguments { get; set; }

    [JsonPropertyName("libraries")]
    public List<QuiltLibrary> Libraries { get; set; }

    public QuiltProfile() { }
}