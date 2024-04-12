using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaDownloadService
{
    public static abstract void Init(
        MCLauncherPath launcherPath,
        MCConfigUrls configUrls,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    );

    public static abstract Task<bool> DownloadJavaRuntimeIndex();
    public static abstract bool LoadJavaRuntimeIndex();
    public static abstract Task<bool> DownloadJavaRuntimeManifest();
    public static abstract bool LoadJavaRuntimeManifest();
    public static abstract Task<bool> DownloadJavaRuntime();
}
