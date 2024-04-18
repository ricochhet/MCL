using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLibraryDownloads(MinecraftArtifact artifact, MinecraftClassifiers classifiers)
{
    [JsonPropertyName("artifact")]
    public MinecraftArtifact Artifact { get; set; } = artifact;

#nullable enable // Classifiers are not present in newer Minecraft versions.
    [JsonPropertyName("classifiers")]
    public MinecraftClassifiers? Classifiers { get; set; } = classifiers;
}
