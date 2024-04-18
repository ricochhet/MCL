using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLibraryRule(string action, MLibraryRuleValue os)
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = action;

#nullable enable // Not all action objects specify an operating system.
    [JsonPropertyName("os")]
    public MLibraryRuleValue? Os { get; set; } = os;
}
