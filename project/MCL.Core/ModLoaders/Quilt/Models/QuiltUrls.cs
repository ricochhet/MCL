namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltUrls
{
    public string VersionManifest { get; set; } = "https://meta.quiltmc.org/v3/versions";
    public string LoaderJar { get; set; } =
        "https://maven.quiltmc.org/repository/release/org/quiltmc/quilt-loader/{0}/quilt-loader-{0}.jar";
    public string LoaderProfile { get; set; } = "https://meta.quiltmc.org/v3/versions/loader/{0}/{1}/profile/json";

    public string ApiLoaderName { get; set; } = "org.quiltmc:quilt-loader";
    public string ApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public QuiltUrls() { }
}
