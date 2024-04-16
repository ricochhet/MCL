using MCL.Core.Models.Java;

namespace MCL.Core.Extensions.Java;

public static class JavaRuntimeFilesExt
{
    public static bool FilesExists(this JavaRuntimeFiles javaRuntimeFiles)
    {
        return javaRuntimeFiles?.Files != null && javaRuntimeFiles.Files?.Count > 0;
    }
}
