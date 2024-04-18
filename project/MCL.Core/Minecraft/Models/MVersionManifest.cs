using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MVersionManifest
{
    [JsonPropertyName("latest")]
    public MLatest Latest { get; set; }

    [JsonPropertyName("versions")]
    public List<MVersion> Versions { get; set; }

    public MVersionManifest() { }
}
