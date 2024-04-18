using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftVersionManifest
{
    [JsonPropertyName("latest")]
    public MinecraftLatest Latest { get; set; }

    [JsonPropertyName("versions")]
    public List<MinecraftVersion> Versions { get; set; }

    public MinecraftVersionManifest() { }
}
