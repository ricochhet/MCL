using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLoggingClient(string argument, MCLoggingFile file, string type)
{
    [JsonPropertyName("argument")]
    public string Argument { get; set; } = argument;

    [JsonPropertyName("file")]
    public MCLoggingFile File { get; set; } = file;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;
}
