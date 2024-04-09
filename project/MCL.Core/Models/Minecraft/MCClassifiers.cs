using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCClassifiers
{
#nullable enable // Classifiers may only be present for one or more operating system.
    [JsonPropertyName("natives-linux")]
    public MCArtifact? NativesLinux { get; set; }

    [JsonPropertyName("natives-osx")]
    public MCArtifact? NativesMacos { get; set; }

    [JsonPropertyName("natives-windows")]
    public MCArtifact? NativesWindows { get; set; }

#nullable disable

    public MCClassifiers(MCArtifact nativesLinux, MCArtifact nativesMacos, MCArtifact nativesWindows)
    {
        NativesLinux = nativesLinux;
        NativesMacos = nativesMacos;
        NativesWindows = nativesWindows;
    }
}
