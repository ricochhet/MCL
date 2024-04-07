namespace MCL.Core.Helpers.Java;

public static class JavaLaunchHelper
{
    public static void Launch(string minecraftArgs, string minecraftPath)
    {
        ProcessHelper.RunProcess("java", minecraftArgs, minecraftPath, false);
    }
}
