using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeObject
{
    [JsonPropertyName("availability")]
    public JavaRuntimeAvailability JavaRuntimeAvailability { get; set; }

    [JsonPropertyName("manifest")]
    public JavaRuntimeManifest JavaRuntimeManifest { get; set; }

    [JsonPropertyName("version")]
    public JavaRuntimeVersion JavaRuntimeVersion { get; set; }

    public JavaRuntimeObject(
        JavaRuntimeAvailability javaRuntimeAvailability,
        JavaRuntimeManifest javaRuntimeManifest,
        JavaRuntimeVersion javaRuntimeVersion
    )
    {
        JavaRuntimeAvailability = javaRuntimeAvailability;
        JavaRuntimeManifest = javaRuntimeManifest;
        JavaRuntimeVersion = javaRuntimeVersion;
    }
}
