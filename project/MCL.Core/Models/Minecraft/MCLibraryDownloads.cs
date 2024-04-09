using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryDownloads(MCArtifact artifact, MCClassifiers classifiers)
{
    [JsonPropertyName("artifact")]
    public MCArtifact Artifact { get; set; } = artifact;

#nullable enable // Classifiers are not present in newer Minecraft versions.
    [JsonPropertyName("classifiers")]
    public MCClassifiers? Classifiers { get; set; } = classifiers;
}
