namespace MCL.Core.Models.Minecraft;

public class MCLibraryDownloads
{
    public MCArtifact Artifact { get; set; }
#nullable enable // Classifiers are not present in newer Minecraft versions.
    public MCClassifiers? Classifiers { get; set; }
}
