using System.Text.Json.Serialization;

namespace MCL.Core.Servers.Paper.Models;

public class PaperBuildChange
{
    [JsonPropertyName("commit")]
    public string Commit { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
