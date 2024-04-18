namespace MCL.Core.Models.Java;

public class JavaSettings
{
    public string JavaExecutable { get; set; } = "java.exe";
    public string JavaHomeEnvironmentVariable { get; set; } = "JAVA_HOME";

    public JavaSettings() { }
}
