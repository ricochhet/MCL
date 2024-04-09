using MCL.Core.Models.Java;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeFilesErr
{
    public static bool Exists(JavaRuntimeFiles javaRuntimeFiles)
    {
        if (javaRuntimeFiles == null)
            return false;

        if (javaRuntimeFiles.Files == null)
            return false;

        if (javaRuntimeFiles.Files.Count <= 0)
            return false;

        return true;
    }
}
