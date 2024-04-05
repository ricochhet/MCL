using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCVersionManifest
{
    [JsonPropertyName("latest")]
    public MCLatest Latest { get; set; }

    [JsonPropertyName("versions")]
    public List<MCVersion> Versions { get; set; }
}
