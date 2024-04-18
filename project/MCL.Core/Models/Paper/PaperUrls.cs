namespace MCL.Core.Models.Paper;

public class PaperUrls
{
    public string PaperVersionManifest { get; set; } = "https://api.papermc.io/v2/projects/{0}/versions/{1}/builds";
    public string PaperJarUrl { get; set; } =
        "https://api.papermc.io/v2/projects/{0}/versions/{1}/builds/{2}/downloads/{3}";

    public PaperUrls() { }
}
