namespace MCL.Core.Models.MinecraftFabric;

public class MCFabricConfigUrls
{
    public string FabricVersionsIndex { get; set; } = "https://meta.fabricmc.net/v2/versions";
    public string FabricLoaderJarUrl { get; set; } =
        "https://maven.fabricmc.net/net/fabricmc/fabric-loader/{0}/fabric-loader-{0}.jar";
}
