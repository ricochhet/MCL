using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCClassifiers
{
    [JsonPropertyName("natives-linux")]
    public MCArtifact NativesLinux { get; set; }

    [JsonPropertyName("natives-osx")]
    public MCArtifact NativesMacos { get; set; }

    [JsonPropertyName("natives-windows")]
    public MCArtifact NativesWindows { get; set; }
}
