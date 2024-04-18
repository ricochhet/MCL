using MCL.Core.Java.Models;

namespace MCL.Core.Java.Extensions;

public static class JavaRuntimeFileExt
{
    public static bool FileExists(this JavaRuntimeFile javaRuntimeFile)
    {
        return !string.IsNullOrWhiteSpace(javaRuntimeFile?.Downloads?.Raw?.URL)
            && !string.IsNullOrWhiteSpace(javaRuntimeFile.Downloads?.Raw?.SHA1);
    }
}
