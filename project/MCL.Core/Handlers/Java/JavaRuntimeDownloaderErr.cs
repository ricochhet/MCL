using MCL.Core.Interfaces;
using MCL.Core.Models.Java;

namespace MCL.Core.Handlers.Java;

public class JavaRuntimeDownloaderErr : IErrorHandleItem<JavaRuntimeFiles>, IErrorHandleItem<JavaRuntimeFile>
{
    public static bool Exists(JavaRuntimeFiles javaRuntimeFiles)
    {
        if (javaRuntimeFiles?.Files == null)
            return false;

        if (javaRuntimeFiles.Files?.Count <= 0)
            return false;

        return true;
    }

    public static bool Exists(JavaRuntimeFile javaRuntimeFile)
    {
        if (string.IsNullOrWhiteSpace(javaRuntimeFile?.Downloads?.Raw?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(javaRuntimeFile.Downloads?.Raw?.SHA1))
            return false;

        return true;
    }
}
