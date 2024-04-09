using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeAvailability
{
    [JsonPropertyName("group")]
    public int Group { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }

    public JavaRuntimeAvailability(int group, int progress)
    {
        Group = group;
        Progress = progress;
    }
}
