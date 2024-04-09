using MCL.Core.Models.Java;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeFileErr
{
    public static bool Exists(JavaRuntimeFile javaRuntimeFile)
    {
        if (javaRuntimeFile.Downloads == null)
            return false;

        if (javaRuntimeFile.Downloads.Raw == null)
            return false;

        if (string.IsNullOrWhiteSpace(javaRuntimeFile.Downloads.Raw.URL))
            return false;

        if (string.IsNullOrWhiteSpace(javaRuntimeFile.Downloads.Raw.SHA1))
            return false;

        return true;
    }
}
