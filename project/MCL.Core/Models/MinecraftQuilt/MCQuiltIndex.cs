using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftQuilt;

public class MCQuiltIndex
{
    [JsonPropertyName("game")]
    public List<MCQuiltGame> Game { get; set; }

    [JsonPropertyName("mappings")]
    public List<MCQuiltMappings> Mappings { get; set; }

    [JsonPropertyName("hashed")]
    public List<MCQuiltHashed> Hashed { get; set; }

    [JsonPropertyName("loader")]
    public List<MCQuiltLoader> Loader { get; set; }

    [JsonPropertyName("installer")]
    public List<MCQuiltInstaller> Installer { get; set; }

    public MCQuiltIndex() { }
}
