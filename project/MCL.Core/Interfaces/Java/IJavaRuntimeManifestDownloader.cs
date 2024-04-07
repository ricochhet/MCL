using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeManifestDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath minecraftPath,
        JavaRuntimePlatformEnum javaRuntimePlatformEnum,
        JavaRuntimeTypeEnum javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    );
    public static abstract string GetJavaRuntimeUrl(
        JavaRuntimeTypeEnum javaRuntimeTypeEnum,
        JavaRuntime javaRuntimePlatform
    );
    public static abstract bool Exists(JavaRuntimeIndex javaRuntimeIndex);

    public static abstract bool JavaRuntimeUrlExists(
        JavaRuntime javaRuntimePlatform,
        List<JavaRuntimeObject> javaRuntimeObjects
    );
}
