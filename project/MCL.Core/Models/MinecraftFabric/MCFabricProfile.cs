using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricProfile
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
    public MCArgument Arguments { get; set; }

    [JsonPropertyName("libraries")]
    public List<MCFabricLibrary> Libraries { get; set; }

    public MCFabricProfile() { }

    public MCFabricProfile(
        string id,
        string inheritsFrom,
        string releaseTime,
        string time,
        string type,
        string mainClass,
        MCArgument arguments,
        List<MCFabricLibrary> libraries
    )
    {
        ID = id;
        InheritsFrom = inheritsFrom;
        ReleaseTime = releaseTime;
        Time = time;
        Type = type;
        MainClass = mainClass;
        Arguments = arguments;
        Libraries = libraries;
    }
}