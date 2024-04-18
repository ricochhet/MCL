using System.Text.Json.Serialization;

namespace MCL.Core.Servers.Paper.Models;

public class PaperDownloadItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("sha256")]
    public string SHA256 { get; set; }
}
