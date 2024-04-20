using System.Collections.Generic;

namespace MCL.Core.Launcher.Models;

public class LauncherInstance
{
    public List<string> Versions { get; set; } = [];
    public List<LauncherModLoader> FabricLoaders { get; set; } = [];
    public List<LauncherModLoader> QuiltLoaders { get; set; } = [];
    public List<string> PaperServerVersions { get; set; } = [];

    public LauncherInstance() { }
}
