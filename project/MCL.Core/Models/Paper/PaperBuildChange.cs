using System.Text.Json.Serialization;

namespace MCL.Core.Models.Paper;

public class PaperBuildChange
{
    [JsonPropertyName("commit")]
    public string Commit { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
