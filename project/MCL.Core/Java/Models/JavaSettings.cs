namespace MCL.Core.Java.Models;

public class JavaSettings
{
    public string Executable { get; set; } = "java.exe";
    public string HomeEnvironmentVariable { get; set; } = "JAVA_HOME";

    public JavaSettings() { }
}
