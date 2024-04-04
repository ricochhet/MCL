using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Config.Minecraft;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class DownloadHelper
{
    public static async Task<bool> DownloadVersionManifestJson(
        MinecraftUrlConfig minecraftUrlConfig,
        string minecraftPath
    )
    {
        return await Request.Download(
            minecraftUrlConfig.URL.VersionManifest,
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );
    }

    public static async Task<bool> DownloadVersionDetailsJson(string minecraftPath, Version version)
    {
        return await Request.Download(
            version.URL,
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );
    }

    public static async Task<bool> DownloadLibraries(
        string minecraftPath,
        PlatformEnum minecraftPlatform,
        List<Library> libraries
    )
    {
        string libPath = Path.Combine(minecraftPath, "libraries");
        foreach (Library lib in libraries)
        {
            if (lib?.Rules?.Count != 0)
            {
                foreach (Rule rule in lib.Rules)
                {
                    if (
                        rule?.Action == RuleEnumResolver.ToString(RuleEnum.ALLOW)
                        && rule?.Os?.Name != PlatformEnumResolver.ToString(minecraftPlatform)
                    )
                    {
                        continue;
                    }
                }
            }

            if (lib.Downloads?.Classifiers != null)
            {
                string classifierDownloadPath = string.Empty;
                string classifierUrl = string.Empty;
                string classifierSha1 = string.Empty;

                switch (minecraftPlatform)
                {
                    case PlatformEnum.WINDOWS:
                        classifierDownloadPath = Path.Combine(libPath, lib.Downloads.Classifiers.NativesWindows.Path);
                        classifierUrl = lib.Downloads.Classifiers.NativesWindows.URL;
                        classifierSha1 = lib.Downloads.Classifiers.NativesWindows.SHA1;
                        break;
                    case PlatformEnum.LINUX:
                        classifierDownloadPath = Path.Combine(libPath, lib.Downloads.Classifiers.NativesLinux.Path);
                        classifierUrl = lib.Downloads.Classifiers.NativesLinux.URL;
                        classifierSha1 = lib.Downloads.Classifiers.NativesLinux.SHA1;
                        break;
                    case PlatformEnum.OSX:
                        classifierDownloadPath = Path.Combine(libPath, lib.Downloads.Classifiers.NativesMacos.Path);
                        classifierUrl = lib.Downloads.Classifiers.NativesMacos.URL;
                        classifierSha1 = lib.Downloads.Classifiers.NativesMacos.SHA1;
                        break;
                }

                bool status = await Request.NewDownloadRequest(classifierDownloadPath, classifierUrl, classifierSha1);

                if (!status)
                    return false;
            }

            string downloadPath = Path.Combine(libPath, lib.Downloads.Artifact.Path);
            return await Request.NewDownloadRequest(
                downloadPath,
                lib.Downloads.Artifact.URL,
                lib.Downloads.Artifact.SHA1
            );
        }

        return true;
    }

    public static async Task<bool> DownloadClient(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPathResolver.ClientJarPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Client.SHA1
        )
        {
            return true;
        }
        return await Request.Download(versionDetails.Downloads.Client.URL, downloadPath);
    }

    public static async Task<bool> DownloadServer(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPathResolver.ServerJarPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Server.SHA1
        )
        {
            return true;
        }

        ServerProperties.NewEula(minecraftPath);
        ServerProperties.NewProperties(minecraftPath);

        return await Request.Download(versionDetails.Downloads.Server.URL, downloadPath);
    }

    public static async Task<bool> DownloadResources(
        string minecraftPath,
        string minecraftResourcesUrl,
        AssetsData assets
    )
    {
        string objectsPath = Path.Combine(MinecraftPathResolver.AssetsPath(minecraftPath), "objects");
        foreach ((_, Asset asset) in assets.Objects)
        {
            string url = $"{minecraftResourcesUrl}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = Path.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            return await Request.NewDownloadRequest(downloadPath, url, asset.Hash);
        }

        return true;
    }

    public static async Task<bool> DownloadIndexJson(string minecraftPath, AssetIndex assetIndex)
    {
        string downloadPath = Path.Combine(
            MinecraftPathResolver.AssetsPath(minecraftPath),
            "indexes",
            assetIndex.ID + ".json"
        );
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == assetIndex.SHA1)
        {
            return true;
        }
        return await Request.Download(assetIndex.URL, downloadPath);
    }
}
