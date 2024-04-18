using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLibraryRule(string action, MinecraftLibraryRuleValue os)
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = action;

#nullable enable // Not all action objects specify an operating system.
    [JsonPropertyName("os")]
    public MinecraftLibraryRuleValue? Os { get; set; } = os;
}
