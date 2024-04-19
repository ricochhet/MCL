using System.Collections.Generic;

namespace MCL.Core.Launcher.Models;

public class InstanceModLoader
{
    public string LoaderVersion { get; set; }
    public List<string> Libraries { get; set; }
}
