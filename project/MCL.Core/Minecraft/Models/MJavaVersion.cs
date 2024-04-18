using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MJavaVersion(string component, int majorVersion)
{
    [JsonPropertyName("component")]
    public string Component { get; set; } = component;

    [JsonPropertyName("majorVersion")]
    public int MajorVersion { get; set; } = majorVersion;
}
