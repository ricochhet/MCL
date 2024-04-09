using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeFile(JavaRuntimeFileDownloads downloads, bool executable, string type)
{
    [JsonPropertyName("downloads")]
    public JavaRuntimeFileDownloads Downloads { get; set; } = downloads;

    [JsonPropertyName("executable")]
    public bool Executable { get; set; } = executable;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;
}
