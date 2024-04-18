using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLibraryDownloads(MArtifact artifact, MClassifiers classifiers)
{
    [JsonPropertyName("artifact")]
    public MArtifact Artifact { get; set; } = artifact;

#nullable enable // Classifiers are not present in newer Minecraft versions.
    [JsonPropertyName("classifiers")]
    public MClassifiers? Classifiers { get; set; } = classifiers;
}
