using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.ModLoaders.Fabric;

public class FabricIndex
{
    [JsonPropertyName("game")]
    public List<FabricGame> Game { get; set; }

    [JsonPropertyName("mappings")]
    public List<FabricMappings> Mappings { get; set; }

    [JsonPropertyName("intermediary")]
    public List<FabricIntermediary> Intermediary { get; set; }

    [JsonPropertyName("loader")]
    public List<FabricLoader> Loader { get; set; }

    [JsonPropertyName("installer")]
    public List<FabricInstaller> Installer { get; set; }

    public FabricIndex() { }
}
