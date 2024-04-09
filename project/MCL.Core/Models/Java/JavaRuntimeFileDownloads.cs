using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeFileDownloads
{
    [JsonPropertyName("lzma")]
    public JavaRuntimeFileDownload Lzma { get; set; }

    [JsonPropertyName("raw")]
    public JavaRuntimeFileDownload Raw { get; set; }

    public JavaRuntimeFileDownloads(JavaRuntimeFileDownload lzma, JavaRuntimeFileDownload raw)
    {
        Lzma = lzma;
        Raw = raw;
    }
}
