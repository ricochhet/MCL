using System.Collections.Generic;
using System.Linq;

namespace MCL.Core.Services.Modding;

public class ModConfig
{
    public string[] FileTypes = [".jar", ".zip", ".rar", ".7z"];
    public string[] CopyOnlyTypes = [".jar"];
    public string[] UnzipAndCopyTypes = [".zip", ".rar", ".7z"];
    public List<string> RegisteredStores = [];

    public ModConfig() { }

    public ModConfig(
        string[] fileTypes,
        string[] copyOnlyTypes,
        string[] unzipAndCopyTypes,
        List<string> registeredStores
    )
    {
        FileTypes = fileTypes;
        CopyOnlyTypes = copyOnlyTypes;
        UnzipAndCopyTypes = unzipAndCopyTypes;
        RegisteredStores = registeredStores;
    }

    public bool IsStoreRegistered(string modStoreName) => RegisteredStores.Contains(modStoreName);
}
