using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLoggingClient
{
    [JsonPropertyName("argument")]
    public string Argument { get; set; }

    [JsonPropertyName("file")]
    public MCLoggingFile File { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
