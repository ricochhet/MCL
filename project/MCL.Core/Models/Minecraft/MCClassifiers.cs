using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCClassifiers(MCArtifact nativesLinux, MCArtifact nativesMacos, MCArtifact nativesWindows)
{
#nullable enable // Classifiers may only be present for one or more operating system.
    [JsonPropertyName("natives-linux")]
    public MCArtifact? NativesLinux { get; set; } = nativesLinux;

    [JsonPropertyName("natives-osx")]
    public MCArtifact? NativesMacos { get; set; } = nativesMacos;

    [JsonPropertyName("natives-windows")]
    public MCArtifact? NativesWindows { get; set; } = nativesWindows;
}
