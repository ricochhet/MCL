using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

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

    public JavaRuntime(
        List<JavaRuntimeObject> javaRuntimeAlpha,
        List<JavaRuntimeObject> javaRuntimeBeta,
        List<JavaRuntimeObject> javaRuntimeDelta,
        List<JavaRuntimeObject> javaRuntimeGamma,
        List<JavaRuntimeObject> javaRuntimeGammaSnapshot,
        List<JavaRuntimeObject> jreLegacy,
        List<JavaRuntimeObject> minecraftJavaExe
    )
    {
        JavaRuntimeAlpha = javaRuntimeAlpha;
        JavaRuntimeBeta = javaRuntimeBeta;
        JavaRuntimeDelta = javaRuntimeDelta;
        JavaRuntimeGamma = javaRuntimeGamma;
        JavaRuntimeGammaSnapshot = javaRuntimeGammaSnapshot;
        JreLegacy = jreLegacy;
        MinecraftJavaExe = minecraftJavaExe;
    }
}
