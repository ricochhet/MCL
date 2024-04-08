using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Java;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Java;

namespace MCL.Core.Providers.Java;

public class JavaDownloadProvider
{
    public JavaRuntimeIndex javaRuntimeIndex = new();
    public JavaRuntimeFiles javaRuntimeFiles = new();
    private static MCLauncherPath launcherPath;
    private static MCConfigUrls configUrls;
    private static JavaRuntimeTypeEnum javaRuntimeType;
    private static JavaRuntimePlatformEnum javaRuntimePlatform;

    public JavaDownloadProvider(
        MCLauncherPath _launcherPath,
        MCConfigUrls _configUrls,
        JavaRuntimeTypeEnum _javaRuntimeType,
        JavaRuntimePlatformEnum _javaRuntimePlatform
    )
    {
        launcherPath = _launcherPath;
        configUrls = _configUrls;
        javaRuntimeType = _javaRuntimeType;
        javaRuntimePlatform = _javaRuntimePlatform;
    }

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadJavaRuntimeIndex())
            return false;

        if (!await DownloadJavaRuntimeManifest())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public async Task<bool> DownloadJavaRuntimeIndex()
    {
        if (!await JavaRuntimeIndexDownloader.Download(launcherPath, configUrls))
        {
            LogBase.Error("Failed to download java runtime index");
            return false;
        }

        javaRuntimeIndex = Json.Read<JavaRuntimeIndex>(
            MinecraftPathResolver.DownloadedJavaRuntimeIndexPath(launcherPath)
        );
        if (javaRuntimeIndex == null)
        {
            LogBase.Error($"Failed to get java runtime index");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadJavaRuntimeManifest()
    {
        if (
            !await JavaRuntimeManifestDownloader.Download(
                launcherPath,
                javaRuntimePlatform,
                javaRuntimeType,
                javaRuntimeIndex
            )
        )
        {
            LogBase.Error("Failed to download java runtime manifest");
            return false;
        }

        javaRuntimeFiles = Json.Read<JavaRuntimeFiles>(
            MinecraftPathResolver.DownloadedJavaRuntimeManifestPath(
                launcherPath,
                JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType)
            )
        );
        if (javaRuntimeFiles == null)
        {
            LogBase.Error($"Failed to get java runtime manifest");
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(launcherPath, javaRuntimeType, javaRuntimeFiles))
        {
            LogBase.Error("Failed to download java runtime");
            return false;
        }

        return true;
    }
}
