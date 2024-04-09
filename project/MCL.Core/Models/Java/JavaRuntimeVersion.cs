using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeVersion
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("released")]
    public string Released { get; set; }

    public JavaRuntimeVersion(string name, string released)
    {
        Name = name;
        Released = released;
    }
}
