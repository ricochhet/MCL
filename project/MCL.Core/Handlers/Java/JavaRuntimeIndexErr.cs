using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeIndexErr
{
    public static bool Exists(JavaRuntimeIndex javaRuntimeIndex)
    {
        if (javaRuntimeIndex == null)
            return false;

        if (javaRuntimeIndex.Gamecore == null)
            return false;

        if (javaRuntimeIndex.Linux == null)
            return false;

        if (javaRuntimeIndex.LinuxI386 == null)
            return false;

        if (javaRuntimeIndex.Macos == null)
            return false;

        if (javaRuntimeIndex.MacosArm64 == null)
            return false;

        if (javaRuntimeIndex.WindowsArm64 == null)
            return false;

        if (javaRuntimeIndex.WindowsX64 == null)
            return false;

        if (javaRuntimeIndex.WindowsX86 == null)
            return false;

        return true;
    }
}
