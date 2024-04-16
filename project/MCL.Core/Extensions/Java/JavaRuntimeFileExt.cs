using MCL.Core.Models.Java;

namespace MCL.Core.Extensions.Java;

public static class JavaRuntimeFileExt
{
    public static bool FileExists(this JavaRuntimeFile javaRuntimeFile)
    {
        return !string.IsNullOrWhiteSpace(javaRuntimeFile?.Downloads?.Raw?.URL)
            && !string.IsNullOrWhiteSpace(javaRuntimeFile.Downloads?.Raw?.SHA1);
    }
}
