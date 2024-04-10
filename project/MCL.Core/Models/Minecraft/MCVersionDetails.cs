using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCVersionDetails
{
    [JsonPropertyName("arguments")]
    public MCArgument Arguments { get; set; }

    [JsonPropertyName("assetIndex")]
    public MCAssetIndex AssetIndex { get; set; }

    [JsonPropertyName("assets")]
    public string Assets { get; set; }

    [JsonPropertyName("complianceLevel")]
    public int ComplianceLevel { get; set; }

    [JsonPropertyName("downloads")]
    public MCDownloads Downloads { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("javaVersion")]
    public JavaVersion JavaVersion { get; set; }

    [JsonPropertyName("libraries")]
    public List<MCLibrary> Libraries { get; set; }

    [JsonPropertyName("logging")]
    public MCLogging Logging { get; set; }

    [JsonPropertyName("mainClass")]
    public string MainClass { get; set; }

    [JsonPropertyName("minimumLauncherVersion")]
    public int MinimumLauncherVersion { get; set; }

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    public MCVersionDetails() { }
}
