namespace MCL.Core.Services.Modding;

public class ModConfig
{
    public string[] FileTypes = [".jar", ".zip", ".rar", ".7z"];

    public ModConfig() { }

    public ModConfig(string[] fileTypes)
    {
        FileTypes = fileTypes;
    }
}
