namespace MCL.Core.ModLoaders.Fabric.Models;

public class FabricUrls
{
    public string VersionManifest { get; set; } = "https://meta.fabricmc.net/v2/versions";
    public string LoaderJar { get; set; } =
        "https://maven.fabricmc.net/net/fabricmc/fabric-loader/{0}/fabric-loader-{0}.jar";
    public string LoaderProfile { get; set; } = "https://meta.fabricmc.net/v2/versions/loader/{0}/{1}/profile/json";

    public string ApiLoaderName { get; set; } = "net.fabricmc:fabric-loader";
    public string ApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public FabricUrls() { }
}
