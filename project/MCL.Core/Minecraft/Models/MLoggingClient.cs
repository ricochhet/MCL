using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLoggingClient(string argument, MLoggingFile file, string type)
{
    [JsonPropertyName("argument")]
    public string Argument { get; set; } = argument;

    [JsonPropertyName("file")]
    public MLoggingFile File { get; set; } = file;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;
}
