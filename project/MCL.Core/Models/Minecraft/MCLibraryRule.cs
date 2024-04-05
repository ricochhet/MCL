using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryRule
{
    public string Action { get; set; }

#nullable enable // Not all action objects specify an operating system.
    [JsonPropertyName("os")]
    public MCLibraryRuleOS? Os { get; set; }
}
