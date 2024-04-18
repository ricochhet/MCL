using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MVersionDetails
{
    [JsonPropertyName("arguments")]
    public MArgument Arguments { get; set; }

    [JsonPropertyName("assetIndex")]
    public MAssetIndex AssetIndex { get; set; }

    [JsonPropertyName("assets")]
    public string Assets { get; set; }

    [JsonPropertyName("complianceLevel")]
    public int ComplianceLevel { get; set; }

    [JsonPropertyName("downloads")]
    public MDownloads Downloads { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("javaVersion")]
    public MJavaVersion JavaVersion { get; set; }

    [JsonPropertyName("libraries")]
    public List<MLibrary> Libraries { get; set; }

    [JsonPropertyName("logging")]
    public MLogging Logging { get; set; }

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

    public MVersionDetails() { }
}
