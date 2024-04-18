using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftVersionDetails
{
    [JsonPropertyName("arguments")]
    public MinecraftArgument Arguments { get; set; }

    [JsonPropertyName("assetIndex")]
    public MinecraftAssetIndex AssetIndex { get; set; }

    [JsonPropertyName("assets")]
    public string Assets { get; set; }

    [JsonPropertyName("complianceLevel")]
    public int ComplianceLevel { get; set; }

    [JsonPropertyName("downloads")]
    public MinecraftDownloads Downloads { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("javaVersion")]
    public MinecraftJavaVersion JavaVersion { get; set; }

    [JsonPropertyName("libraries")]
    public List<MinecraftLibrary> Libraries { get; set; }

    [JsonPropertyName("logging")]
    public MinecraftLogging Logging { get; set; }

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

    public MinecraftVersionDetails() { }
}
