using System.Collections.Generic;

namespace MCL.Core.Services.Modding;

public class ModConfig
{
    public string[] FileTypes { get; set; } = [".jar", ".zip", ".rar", ".7z"];
    public string[] CopyOnlyTypes { get; set; } = [".jar"];
    public string[] UnzipAndCopyTypes { get; set; } = [".zip", ".rar", ".7z"];
    public List<string> RegisteredStores { get; set; } = [];
    public List<string> RegisteredDeployPaths { get; set; } = [];

    public ModConfig() { }

    public bool IsStoreRegistered(string modStoreName) => RegisteredStores.Contains(modStoreName);

    public bool IsDeployPathRegistered(string deployPathName) => RegisteredDeployPaths.Contains(deployPathName);
}
