using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLoggingClient(string argument, MinecraftLoggingFile file, string type)
{
    [JsonPropertyName("argument")]
    public string Argument { get; set; } = argument;

    [JsonPropertyName("file")]
    public MinecraftLoggingFile File { get; set; } = file;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;
}
