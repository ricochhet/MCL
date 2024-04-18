namespace MCL.Core.FileExtractors.Models;

public class SevenZipSettings
{
    public string Executable { get; set; } = "7z";
    public string ExtractArguments { get; set; } = "x \"{0}\" -o\"{1}\" -r";

    public SevenZipSettings() { }
}
