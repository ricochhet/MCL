using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntime
{
    [JsonPropertyName("java-runtime-alpha")]
    public List<JavaRuntimeObject> JavaRuntimeAlpha { get; set; }

    [JsonPropertyName("java-runtime-beta")]
    public List<JavaRuntimeObject> JavaRuntimeBeta { get; set; }

    [JsonPropertyName("java-runtime-delta")]
    public List<JavaRuntimeObject> JavaRuntimeDelta { get; set; }

    [JsonPropertyName("java-runtime-gamma")]
    public List<JavaRuntimeObject> JavaRuntimeGamma { get; set; }

    [JsonPropertyName("java-runtime-gamma-snapshot")]
    public List<JavaRuntimeObject> JavaRuntimeGammaSnapshot { get; set; }

    [JsonPropertyName("jre-legacy")]
    public List<JavaRuntimeObject> JreLegacy { get; set; }

    [JsonPropertyName("minecraft-java-exe")]
    public List<JavaRuntimeObject> MinecraftJavaExe { get; set; }
}
