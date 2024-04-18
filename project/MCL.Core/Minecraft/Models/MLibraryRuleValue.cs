using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLibraryRuleValue(string name)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
}
