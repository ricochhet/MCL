using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeVersion(string name, string released)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("released")]
    public string Released { get; set; } = released;
}
