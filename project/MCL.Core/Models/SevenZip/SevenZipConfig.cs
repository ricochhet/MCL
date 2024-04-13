namespace MCL.Core.Models.Java;

public class SevenZipConfig
{
    public string SevenZipExecutable { get; set; } = "7z";
    public string SevenZipExtractArgs { get; set; } = "x \"{0}\" -o\"{1}\" -r";

    public SevenZipConfig() { }
}
