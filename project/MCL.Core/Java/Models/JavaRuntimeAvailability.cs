using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntimeAvailability(int group, int progress)
{
    [JsonPropertyName("group")]
    public int Group { get; set; } = group;

    [JsonPropertyName("progress")]
    public int Progress { get; set; } = progress;
}
