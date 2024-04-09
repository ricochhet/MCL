using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibrary
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("downloads")]
    public MCLibraryDownloads Downloads { get; set; }

    [JsonPropertyName("rules")]
    public List<MCLibraryRule> Rules { get; set; }

#nullable enable // Natives object typically doesn't exist for newer versions.

    [JsonPropertyName("natives")]
    public MCLibraryNatives? Natives { get; set; }

#nullable disable

    public MCLibrary(string name, MCLibraryDownloads downloads, List<MCLibraryRule> rules, MCLibraryNatives natives)
    {
        Name = name;
        Downloads = downloads;
        Rules = rules;
        Natives = natives;
    }
}
