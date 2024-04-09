namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricConfigUrls
{
    public string FabricVersionsIndex { get; set; } = "https://meta.fabricmc.net/v2/versions";
    public string FabricLoaderJarUrl { get; set; } =
        "https://maven.fabricmc.net/net/fabricmc/fabric-loader/{0}/fabric-loader-{0}.jar";
    public string FabricLoaderProfileUrl { get; set; } =
        "https://meta.fabricmc.net/v2/versions/loader/{0}/{1}/profile/json";

    public string FabricApiLoaderName { get; set; } = "net.fabricmc:fabric-loader";
    public string FabricApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public MCFabricConfigUrls() { }

    public MCFabricConfigUrls(
        string fabricVersionsIndex,
        string fabricLoaderJarUrl,
        string fabricLoaderProfileUrl,
        string fabricApiLoaderName,
        string fabricApiIntermediaryName
    )
    {
        FabricVersionsIndex = fabricVersionsIndex;
        FabricLoaderJarUrl = fabricLoaderJarUrl;
        FabricLoaderProfileUrl = fabricLoaderProfileUrl;
        FabricApiLoaderName = fabricApiLoaderName;
        FabricApiIntermediaryName = fabricApiIntermediaryName;
    }
}
