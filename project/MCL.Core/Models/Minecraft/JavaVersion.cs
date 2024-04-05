using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaVersion
{
    [JsonPropertyName("component")]
    public string Component { get; set; }

    [JsonPropertyName("majorVersion")]
    public int MajorVersion { get; set; }
}
