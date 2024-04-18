namespace MCL.Core.FileExtractors.Models;

public class SevenZipSettings
{
    public string SevenZipExecutable { get; set; } = "7z";
    public string SevenZipExtractArgs { get; set; } = "x \"{0}\" -o\"{1}\" -r";

    public SevenZipSettings() { }
}
