using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLibraryRuleValue(string name)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
}
