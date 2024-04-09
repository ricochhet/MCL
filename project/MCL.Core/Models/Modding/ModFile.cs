namespace MCL.Core.Services.Modding;

public class ModFile
{
    public string ModPath { get; set; }
    public string SHA1 { get; set; }
    public int Priority { get; set; }

    public ModFile() { }

    public ModFile(string modPath, string sha1, int priority = 0)
    {
        ModPath = modPath;
        SHA1 = sha1;
        Priority = priority;
    }
}
