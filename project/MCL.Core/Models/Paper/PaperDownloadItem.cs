using System.Text.Json.Serialization;

namespace MCL.Core.Models.Paper;

public class PaperDownloadItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("sha256")]
    public string SHA256 { get; set; }
}
