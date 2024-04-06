using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntimeAvailability
{
    [JsonPropertyName("group")]
    public int Group { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }
}
