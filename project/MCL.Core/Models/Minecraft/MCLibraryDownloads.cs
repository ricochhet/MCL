using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLibraryDownloads
{
    [JsonPropertyName("artifact")]
    public MCArtifact Artifact { get; set; }

#nullable enable // Classifiers are not present in newer Minecraft versions.
    [JsonPropertyName("classifiers")]
    public MCClassifiers? Classifiers { get; set; }

#nullable disable

    public MCLibraryDownloads(MCArtifact artifact, MCClassifiers classifiers)
    {
        Artifact = artifact;
        Classifiers = classifiers;
    }
}
