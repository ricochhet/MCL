using System.Collections.Generic;

namespace MCL.Core.Models.Minecraft;

public class MCVersionDetails
{
    public Dictionary<string, object> Arguments { get; set; }
    public MCAssetIndex AssetIndex { get; set; }
    public string Assets { get; set; }
    public int ComplianceLevel { get; set; }
    public MCDownloads Downloads { get; set; }
    public string ID { get; set; }
    public List<MCLibrary> Libraries { get; set; }
    public MCLogging Logging { get; set; }
    public int MinimumLauncherVersion { get; set; }
    public string ReleaseTime { get; set; }
    public string Time { get; set; }
    public string Type { get; set; }
    public string MainClass { get; set; }
}
