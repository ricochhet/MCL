using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftArgument(List<object> game, List<object> jvm)
{
    [JsonPropertyName("game")]
    public List<object> Game { get; set; } = game;

    [JsonPropertyName("jvm")]
    public List<object> JVM { get; set; } = jvm;
}
