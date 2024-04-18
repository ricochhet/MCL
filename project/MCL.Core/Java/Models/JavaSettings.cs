namespace MCL.Core.Java.Models;

public class JavaSettings
{
    public string JavaExecutable { get; set; } = "java.exe";
    public string JavaHomeEnvironmentVariable { get; set; } = "JAVA_HOME";

    public JavaSettings() { }
}
