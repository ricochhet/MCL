using System.Text.Json.Serialization;

namespace MCL.Core.Servers.Paper.Models;

public class PaperDownloads
{
    [JsonPropertyName("application")]
    public PaperDownloadItem Application { get; set; }

    [JsonPropertyName("mojang-mappings")]
    public PaperDownloadItem MojangMappings { get; set; }
}
