using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLibrary(string name, MLibraryDownloads downloads, List<MLibraryRule> rules, MLibraryNatives natives)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("downloads")]
    public MLibraryDownloads Downloads { get; set; } = downloads;

    [JsonPropertyName("rules")]
    public List<MLibraryRule> Rules { get; set; } = rules;

#nullable enable // Natives object typically doesn't exist for newer versions.

    [JsonPropertyName("natives")]
    public MLibraryNatives? Natives { get; set; } = natives;
}
