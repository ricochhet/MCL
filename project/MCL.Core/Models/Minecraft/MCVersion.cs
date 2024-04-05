namespace MCL.Core.Models.Minecraft;

public class MCVersion
{
    public string ID { get; set; }
    public string Type { get; set; }
    public string URL { get; set; }
    public string Time { get; set; }
    public string ReleaseTime { get; set; }
    public JavaVersion JavaVersion { get; set; }
}
