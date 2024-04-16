using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.Java;

public interface IJavaRuntimeManifestDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimePlatform javaRuntimePlatform,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimeIndex javaRuntimeIndex
    );
    public static abstract string GetJavaRuntimeUrl(JavaRuntimeType javaRuntimeType, JavaRuntime javaRuntime);
}
