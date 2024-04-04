using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Resolvers;

namespace MCL.Core.Helpers;

public static class DownloadHelper
{
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
                    if (rule?.Action == RuleEnumResolver.ToString(RuleEnum.ALLOW) && rule?.Os?.Name != PlatformEnumResolver.ToString(minecraftPlatform))
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

                bool status = await NewDownloadRequest(classifierDownloadPath, classifierUrl, classifierSha1);

                if (!status)
                    return false;
            }

            string downloadPath = Path.Combine(libPath, lib.Downloads.Artifact.Path);
            return await NewDownloadRequest(downloadPath, lib.Downloads.Artifact.URL, lib.Downloads.Artifact.SHA1);
        }

        return true;
    }

    public static async Task<bool> DownloadClient(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPath.ClientPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Client.SHA1
        )
        {
            return true;
        }
        return await WebRequest.Download(versionDetails.Downloads.Client.URL, downloadPath);
    }

    public static async Task<bool> DownloadServer(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPath.ServerPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Server.SHA1
        )
        {
            return true;
        }

        MinecraftServerProperties.NewEula(minecraftPath);
        MinecraftServerProperties.NewProperties(minecraftPath);

        return await WebRequest.Download(versionDetails.Downloads.Server.URL, downloadPath);
    }

    public static async Task<bool> DownloadResources(
        string minecraftPath,
        string minecraftResourcesUrl,
        AssetsData assets
    )
    {
        string objectsPath = Path.Combine(MinecraftPath.AssetsPath(minecraftPath), "objects");
        foreach ((_, Asset asset) in assets.Objects)
        {
            string url = $"{minecraftResourcesUrl}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = Path.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            return await NewDownloadRequest(downloadPath, url, asset.Hash);
        }

        return true;
    }

    public static async Task<bool> DownloadIndexJson(string minecraftPath, AssetIndex assetIndex)
    {
        string downloadPath = Path.Combine(MinecraftPath.AssetsPath(minecraftPath), "indexes", assetIndex.ID + ".json");
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == assetIndex.SHA1)
        {
            return true;
        }
        return await WebRequest.Download(assetIndex.URL, downloadPath);
    }

    private static async Task<bool> NewDownloadRequest(string downloadPath, string url, string sha1)
    {
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == sha1)
        {
            return true;
        }
        else if (!await WebRequest.Download(url, downloadPath))
        {
            return false;
        }

        return true;
    }
}
