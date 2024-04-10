using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeManifestDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimePlatform javaRuntimePlatformEnum,
        JavaRuntimeType javaRuntimeTypeEnum,
        JavaRuntimeIndex javaRuntimeIndex
    );
    public static abstract string GetJavaRuntimeUrl(JavaRuntimeType javaRuntimeTypeEnum, JavaRuntime javaRuntime);
}
