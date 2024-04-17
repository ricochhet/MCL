using System.Collections.Generic;

namespace MCL.Core.Models.Modding;

public class ModConfig
{
    public string[] CopyOnlyTypes { get; set; } = [".jar"];
    public string[] UnzipAndCopyTypes { get; set; } = [".zip", ".rar", ".7z"];
    public List<string> ModStores { get; set; } = [];
    public List<string> DeployPaths { get; set; } = [];

    public ModConfig() { }

    public bool IsStoreSaved(string modStoreName) => ModStores.Contains(modStoreName);

    public bool IsDeployPathSaved(string deployPathName) => DeployPaths.Contains(deployPathName);
}
