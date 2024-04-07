using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntimeFile
{
    [JsonPropertyName("downloads")]
    public JavaRuntimeFileDownloads Downloads { get; set; }

    [JsonPropertyName("executable")]
    public bool Executable { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
