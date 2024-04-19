using System.Collections.Generic;

namespace MCL.Core.Launcher.Models;

public class LauncherModLoader
{
    public string LoaderVersion { get; set; } = string.Empty;
    public List<string> Libraries { get; set; } = [];
}
