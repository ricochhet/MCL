using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Paper;

public class PaperVersionManifest
{
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; }

    [JsonPropertyName("project_name")]
    public string ProjectName { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("builds")]
    public List<PaperBuild> Builds { get; set; }
}
