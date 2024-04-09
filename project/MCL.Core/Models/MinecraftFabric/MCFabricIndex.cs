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

    public MCFabricIndex(
        List<MCFabricGame> game,
        List<MCFabricMappings> mappings,
        List<MCFabricIntermediary> intermediary,
        List<MCFabricLoader> loader,
        List<MCFabricInstaller> installer
    )
    {
        Game = game;
        Mappings = mappings;
        Intermediary = intermediary;
        Loader = loader;
        Installer = installer;
    }
}
