using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryNatives
{
    [JsonPropertyName("windows")]
    public string Windows { get; set; }

    [JsonPropertyName("osx")]
    public string Osx { get; set; }

    [JsonPropertyName("linux")]
    public string Linux { get; set; }
}
