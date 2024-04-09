using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryRuleValue
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    public MCLibraryRuleValue(string name)
    {
        Name = name;
    }
}
