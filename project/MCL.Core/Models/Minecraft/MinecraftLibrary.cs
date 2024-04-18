using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLibrary(
    string name,
    MinecraftLibraryDownloads downloads,
    List<MinecraftLibraryRule> rules,
    MinecraftLibraryNatives natives
)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("downloads")]
    public MinecraftLibraryDownloads Downloads { get; set; } = downloads;

    [JsonPropertyName("rules")]
    public List<MinecraftLibraryRule> Rules { get; set; } = rules;

#nullable enable // Natives object typically doesn't exist for newer versions.

    [JsonPropertyName("natives")]
    public MinecraftLibraryNatives? Natives { get; set; } = natives;
}
