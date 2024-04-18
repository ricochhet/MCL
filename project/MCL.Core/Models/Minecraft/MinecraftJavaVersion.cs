using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftJavaVersion(string component, int majorVersion)
{
    [JsonPropertyName("component")]
    public string Component { get; set; } = component;

    [JsonPropertyName("majorVersion")]
    public int MajorVersion { get; set; } = majorVersion;
}
