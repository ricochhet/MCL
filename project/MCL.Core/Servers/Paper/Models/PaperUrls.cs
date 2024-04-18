namespace MCL.Core.Servers.Paper.Models;

public class PaperUrls
{
    public string VersionManifest { get; set; } = "https://api.papermc.io/v2/projects/{0}/versions/{1}/builds";
    public string PaperJar { get; set; } =
        "https://api.papermc.io/v2/projects/{0}/versions/{1}/builds/{2}/downloads/{3}";

    public PaperUrls() { }
}
