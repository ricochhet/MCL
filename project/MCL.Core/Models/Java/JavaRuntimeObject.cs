using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class JavaRuntimeObject
{
    [JsonPropertyName("availability")]
    public JavaRuntimeAvailability JavaRuntimeAvailability { get; set; }

    [JsonPropertyName("manifest")]
    public JavaRuntimeManifest JavaRuntimeManifest { get; set; }

    [JsonPropertyName("version")]
    public JavaRuntimeVersion JavaRuntimeVersion { get; set; }
}
