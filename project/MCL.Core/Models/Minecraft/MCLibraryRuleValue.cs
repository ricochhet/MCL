using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryRuleValue(string name)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
}
