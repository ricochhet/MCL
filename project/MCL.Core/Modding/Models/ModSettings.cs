using System.Collections.Generic;

namespace MCL.Core.Modding.Models;

public class ModSettings
{
    public List<string> CopyOnlyTypes { get; set; } = [".jar"];
    public List<string> UnzipAndCopyTypes { get; set; } = [".zip", ".rar", ".7z"];
    public List<string> ModStores { get; set; } = [];
    public List<string> DeployPaths { get; set; } = [];

    public ModSettings() { }

    public bool IsStoreSaved(string modStoreName) => ModStores.Contains(modStoreName);

    public bool IsDeployPathSaved(string deployPathName) => DeployPaths.Contains(deployPathName);
}
