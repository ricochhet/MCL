using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MClassifiers(MArtifact nativesLinux, MArtifact nativesMacos, MArtifact nativesWindows)
{
#nullable enable // Classifiers may only be present for one or more operating system.
    [JsonPropertyName("natives-linux")]
    public MArtifact? NativesLinux { get; set; } = nativesLinux;

    [JsonPropertyName("natives-osx")]
    public MArtifact? NativesMacos { get; set; } = nativesMacos;

    [JsonPropertyName("natives-windows")]
    public MArtifact? NativesWindows { get; set; } = nativesWindows;
}
