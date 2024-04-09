using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeIndex
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

    public JavaRuntimeIndex() { }

    public JavaRuntimeIndex(
        JavaRuntime gamecore,
        JavaRuntime linux,
        JavaRuntime linuxI386,
        JavaRuntime macos,
        JavaRuntime macosArm64,
        JavaRuntime windowsArm64,
        JavaRuntime windowsX64,
        JavaRuntime windowsX86
    )
    {
        Gamecore = gamecore;
        Linux = linux;
        LinuxI386 = linuxI386;
        Macos = macos;
        MacosArm64 = macosArm64;
        WindowsArm64 = windowsArm64;
        WindowsX64 = windowsX64;
        WindowsX86 = windowsX86;
    }
}
