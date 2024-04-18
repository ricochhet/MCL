using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntimeObject(
    JavaRuntimeAvailability javaRuntimeAvailability,
    JavaRuntimeManifest javaRuntimeManifest,
    JavaRuntimeVersion javaRuntimeVersion
)
{
    [JsonPropertyName("availability")]
    public JavaRuntimeAvailability JavaRuntimeAvailability { get; set; } = javaRuntimeAvailability;

    [JsonPropertyName("manifest")]
    public JavaRuntimeManifest JavaRuntimeManifest { get; set; } = javaRuntimeManifest;

    [JsonPropertyName("version")]
    public JavaRuntimeVersion JavaRuntimeVersion { get; set; } = javaRuntimeVersion;
}
