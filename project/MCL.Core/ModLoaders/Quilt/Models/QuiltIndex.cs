using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltIndex
{
    [JsonPropertyName("game")]
    public List<QuiltGame> Game { get; set; }

    [JsonPropertyName("mappings")]
    public List<QuiltMappings> Mappings { get; set; }

    [JsonPropertyName("hashed")]
    public List<QuiltHashed> Hashed { get; set; }

    [JsonPropertyName("loader")]
    public List<QuiltLoader> Loader { get; set; }

    [JsonPropertyName("installer")]
    public List<QuiltInstaller> Installer { get; set; }

    public QuiltIndex() { }
}
