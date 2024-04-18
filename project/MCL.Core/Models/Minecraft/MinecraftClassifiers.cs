using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftClassifiers(
    MinecraftArtifact nativesLinux,
    MinecraftArtifact nativesMacos,
    MinecraftArtifact nativesWindows
)
{
#nullable enable // Classifiers may only be present for one or more operating system.
    [JsonPropertyName("natives-linux")]
    public MinecraftArtifact? NativesLinux { get; set; } = nativesLinux;

    [JsonPropertyName("natives-osx")]
    public MinecraftArtifact? NativesMacos { get; set; } = nativesMacos;

    [JsonPropertyName("natives-windows")]
    public MinecraftArtifact? NativesWindows { get; set; } = nativesWindows;
}
