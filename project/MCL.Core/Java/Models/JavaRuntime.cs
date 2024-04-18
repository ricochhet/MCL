using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntime(
    List<JavaRuntimeObject> javaRuntimeAlpha,
    List<JavaRuntimeObject> javaRuntimeBeta,
    List<JavaRuntimeObject> javaRuntimeDelta,
    List<JavaRuntimeObject> javaRuntimeGamma,
    List<JavaRuntimeObject> javaRuntimeGammaSnapshot,
    List<JavaRuntimeObject> jreLegacy,
    List<JavaRuntimeObject> minecraftJavaExe
)
{
    [JsonPropertyName("java-runtime-alpha")]
    public List<JavaRuntimeObject> JavaRuntimeAlpha { get; set; } = javaRuntimeAlpha;

    [JsonPropertyName("java-runtime-beta")]
    public List<JavaRuntimeObject> JavaRuntimeBeta { get; set; } = javaRuntimeBeta;

    [JsonPropertyName("java-runtime-delta")]
    public List<JavaRuntimeObject> JavaRuntimeDelta { get; set; } = javaRuntimeDelta;

    [JsonPropertyName("java-runtime-gamma")]
    public List<JavaRuntimeObject> JavaRuntimeGamma { get; set; } = javaRuntimeGamma;

    [JsonPropertyName("java-runtime-gamma-snapshot")]
    public List<JavaRuntimeObject> JavaRuntimeGammaSnapshot { get; set; } = javaRuntimeGammaSnapshot;

    [JsonPropertyName("jre-legacy")]
    public List<JavaRuntimeObject> JreLegacy { get; set; } = jreLegacy;

    [JsonPropertyName("minecraft-java-exe")]
    public List<JavaRuntimeObject> MinecraftJavaExe { get; set; } = minecraftJavaExe;
}
