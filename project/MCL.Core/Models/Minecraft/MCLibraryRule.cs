using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryRule
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

#nullable enable // Not all action objects specify an operating system.
    [JsonPropertyName("os")]
    public MCLibraryRuleValue? Os { get; set; }

#nullable disable

    public MCLibraryRule(string action, MCLibraryRuleValue os)
    {
        Action = action;
        Os = os;
    }
}
