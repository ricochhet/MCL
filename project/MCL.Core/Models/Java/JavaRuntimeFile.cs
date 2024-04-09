using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeFile
{
    [JsonPropertyName("downloads")]
    public JavaRuntimeFileDownloads Downloads { get; set; }

    [JsonPropertyName("executable")]
    public bool Executable { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    public JavaRuntimeFile(JavaRuntimeFileDownloads downloads, bool executable, string type)
    {
        Downloads = downloads;
        Executable = executable;
        Type = type;
    }
}
