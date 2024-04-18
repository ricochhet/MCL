using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Servers.Paper.Models;

public class PaperBuild
{
    [JsonPropertyName("build")]
    public int Build { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("channel")]
    public string Channel { get; set; }

    [JsonPropertyName("promoted")]
    public bool Promoted { get; set; }

    [JsonPropertyName("changes")]
    public List<PaperBuildChange> Changes { get; set; }

    [JsonPropertyName("downloads")]
    public PaperDownloads Downloads { get; set; }
}
