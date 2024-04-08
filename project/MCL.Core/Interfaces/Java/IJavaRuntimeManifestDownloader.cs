using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeManifestDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimePlatformEnum javaRuntimePlatformEnum,
        JavaRuntimeTypeEnum javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    );
    public static abstract string GetJavaRuntimeUrl(JavaRuntimeTypeEnum javaRuntimeTypeEnum, JavaRuntime javaRuntime);
    public static abstract bool Exists(JavaRuntimeIndex javaRuntimeIndex);

    public static abstract bool JavaRuntimeUrlExists(
        JavaRuntime javaRuntime,
        List<JavaRuntimeObject> javaRuntimeObjects
    );
}
