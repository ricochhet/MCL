using MCL.Core.Models.Java;

namespace MCL.Core.Extensions.Java;

public static class JavaRuntimeIndexExt
{
    public static bool JavaRuntimeExists(this JavaRuntimeIndex javaRuntimeIndex)
    {
        if (javaRuntimeIndex?.Gamecore == null)
            return false;

        if (javaRuntimeIndex.Linux == null || javaRuntimeIndex.LinuxI386 == null)
            return false;

        if (javaRuntimeIndex.Macos == null || javaRuntimeIndex.MacosArm64 == null)
            return false;

        return javaRuntimeIndex.WindowsArm64 != null
            && javaRuntimeIndex.WindowsX64 != null
            && javaRuntimeIndex.WindowsX86 != null;
    }
}
