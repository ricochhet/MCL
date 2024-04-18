using MCL.Core.Java.Models;

namespace MCL.Core.Java.Extensions;

public static class JavaRuntimeFilesExt
{
    public static bool FilesExists(this JavaVersionDetails javaRuntimeFiles)
    {
        return javaRuntimeFiles?.Files != null && javaRuntimeFiles.Files?.Count > 0;
    }
}
