using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCArgument
{
    [JsonPropertyName("game")]
    public List<object> Game { get; set; }

    [JsonPropertyName("jvm")]
    public List<object> JVM { get; set; }
}
