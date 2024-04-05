using System.Collections.Generic;

namespace MCL.Core.Models.Minecraft;

public class MCVersionManifest
{
    public MCLatest Latest { get; set; }
    public List<MCVersion> Versions { get; set; }
}
