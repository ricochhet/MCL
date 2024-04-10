namespace MCL.Core.Models.Java;

public class JavaConfig
{
    public string JavaExecutable { get; set; } = "java.exe";
    public string JavaHomeEnvironmentVariable { get; set; } = "JAVA_HOME";

    public JavaConfig() { }
}
