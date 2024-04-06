using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntimeFileDownloads
{
    [JsonPropertyName("lzma")]
    public JavaRuntimeFileDownload Lzma { get; set; }

    [JsonPropertyName("raw")]
    public JavaRuntimeFileDownload Raw { get; set; }
}
