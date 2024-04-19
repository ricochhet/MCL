using System.Collections.Generic;

namespace MCL.Core.Launcher.Models;

public class LauncherModLoader
{
    public string Version { get; set; } = string.Empty;
    public List<string> Libraries { get; set; } = [];
}
