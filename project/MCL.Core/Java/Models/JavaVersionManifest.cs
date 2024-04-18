using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaVersionManifest
{
    [JsonPropertyName("gamecore")]
    public JavaRuntime Gamecore { get; set; }

    [JsonPropertyName("linux")]
    public JavaRuntime Linux { get; set; }

    [JsonPropertyName("linux-i386")]
    public JavaRuntime LinuxI386 { get; set; }

    [JsonPropertyName("mac-os")]
    public JavaRuntime Macos { get; set; }

    [JsonPropertyName("mac-os-arm64")]
    public JavaRuntime MacosArm64 { get; set; }

    [JsonPropertyName("windows-arm64")]
    public JavaRuntime WindowsArm64 { get; set; }

    [JsonPropertyName("windows-x64")]
    public JavaRuntime WindowsX64 { get; set; }

    [JsonPropertyName("windows-x86")]
    public JavaRuntime WindowsX86 { get; set; }

    public JavaVersionManifest() { }
}
