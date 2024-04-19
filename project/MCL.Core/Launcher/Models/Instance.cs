using System.Collections.Generic;

namespace MCL.Core.Launcher.Models;

public class Instance
{
    public List<string> Versions { get; set; }
    public List<InstanceModLoader> FabricLoaders { get; set; }
    public List<InstanceModLoader> QuiltLoaders { get; set; }
}
