using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricIndex
{
    [JsonPropertyName("game")]
    public List<MCFabricGame> Game { get; set; }

    [JsonPropertyName("mappings")]
    public List<MCFabricMappings> Mappings { get; set; }

    [JsonPropertyName("intermediary")]
    public List<MCFabricIntermediary> Intermediary { get; set; }

    [JsonPropertyName("loader")]
    public List<MCFabricLoader> Loader { get; set; }

    [JsonPropertyName("installer")]
    public List<MCFabricInstaller> Installer { get; set; }

    public MCFabricIndex() { }
}
