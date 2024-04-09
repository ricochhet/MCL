using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryNatives(string windows, string osx, string linux)
{
    [JsonPropertyName("windows")]
    public string Windows { get; set; } = windows;

    [JsonPropertyName("osx")]
    public string Osx { get; set; } = osx;

    [JsonPropertyName("linux")]
    public string Linux { get; set; } = linux;
}
