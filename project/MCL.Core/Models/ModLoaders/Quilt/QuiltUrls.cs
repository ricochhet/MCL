namespace MCL.Core.Models.ModLoaders.Quilt;

public class QuiltUrls
{
    public string QuiltVersionsIndex { get; set; } = "https://meta.quiltmc.org/v3/versions";
    public string QuiltLoaderJarUrl { get; set; } =
        "https://maven.quiltmc.org/repository/release/org/quiltmc/quilt-loader/{0}/quilt-loader-{0}.jar";
    public string QuiltLoaderProfileUrl { get; set; } =
        "https://meta.quiltmc.org/v3/versions/loader/{0}/{1}/profile/json";

    public string QuiltApiLoaderName { get; set; } = "org.quiltmc:quilt-loader";
    public string QuiltApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public QuiltUrls() { }
}
