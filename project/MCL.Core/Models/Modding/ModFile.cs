using MCL.Core.Enums.Services;

namespace MCL.Core.Models.Modding;

public class ModFile
{
    public string ModPath { get; set; }
    public string SHA1 { get; set; }
    public ModRule ModRule { get; set; }
    public int Priority { get; set; }

    public ModFile() { }

    public ModFile(string modPath, string sha1, ModRule modRule, int priority = 0)
    {
        ModPath = modPath;
        SHA1 = sha1;
        ModRule = modRule;
        Priority = priority;
    }
}
