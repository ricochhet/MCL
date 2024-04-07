using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Java;

namespace MCL.Core.Providers;

public class JavaDownloadProvider
{
    public JavaRuntimeIndex javaRuntimeIndex = new();
    public JavaRuntimeFiles javaRuntimeFiles = new();
    private static MCLauncherPath minecraftPath;
    private static MCConfigUrls minecraftUrls;
    private static JavaRuntimeTypeEnum javaRuntimeType;
    private static JavaRuntimePlatformEnum javaRuntimePlatform;

    public JavaDownloadProvider(
        MCLauncherPath _minecraftPath,
        MCConfigUrls _minecraftUrls,
        JavaRuntimeTypeEnum _javaRuntimeType,
        JavaRuntimePlatformEnum _javaRuntimePlatform
    )
    {
        minecraftPath = _minecraftPath;
        minecraftUrls = _minecraftUrls;
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
        if (!await JavaRuntimeIndexDownloader.Download(minecraftPath, minecraftUrls))
        {
            LogBase.Error("Failed to download java runtime index");
            return false;
        }

        javaRuntimeIndex = Json.Read<JavaRuntimeIndex>(
            MinecraftPathResolver.DownloadedJavaRuntimeIndexPath(minecraftPath)
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
                minecraftPath,
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
                minecraftPath,
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
        if (!await JavaRuntimeDownloader.Download(minecraftPath, javaRuntimeType, javaRuntimeFiles))
        {
            LogBase.Error("Failed to download java runtime");
            return false;
        }

        return true;
    }
}
