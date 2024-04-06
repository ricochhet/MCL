using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntimeVersion
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("released")]
    public string Released { get; set; }
}
