using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaDownloadService
{
    public static abstract void Init(
        MCLauncherPath _launcherPath,
        MCConfigUrls _configUrls,
        JavaRuntimeType _javaRuntimeType,
        JavaRuntimePlatform _javaRuntimePlatform
    );

    public static abstract Task<bool> DownloadJavaRuntimeIndex();
    public static abstract bool LoadJavaRuntimeIndex();
    public static abstract Task<bool> DownloadJavaRuntimeManifest();
    public static abstract bool LoadJavaRuntimeManifest();
    public static abstract Task<bool> DownloadJavaRuntime();
}
