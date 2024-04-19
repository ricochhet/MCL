using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntimeFileDownloads(JavaRuntimeFileDownload lzma, JavaRuntimeFileDownload raw)
{
    [JsonPropertyName("lzma")]
    public JavaRuntimeFileDownload Lzma { get; set; } = lzma;

    [JsonPropertyName("raw")]
    public JavaRuntimeFileDownload Raw { get; set; } = raw;
}