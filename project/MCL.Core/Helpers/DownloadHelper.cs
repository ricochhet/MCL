using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;

namespace MCL.Core.Helpers;

public static class DownloadHelper
{
    public static async Task<bool> DownloadLibraries(
        string minecraftPath,
        string minecraftPlatform,
        List<Library> libraries
    )
    {
        string libPath = Path.Combine(minecraftPath, "libraries");
        foreach (Library lib in libraries)
        {
            if (lib.Rules != null)
            {
                if (lib.Rules.Count != 0)
                {
                    if (lib.Rules[0].Os.Name != minecraftPlatform)
                    {
                        continue;
                    }
                }
            }

            string downloadPath = Path.Combine(libPath, lib.Downloads.Artifact.Path);
            if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == lib.Downloads.Artifact.SHA1)
            {
                return true;
            }
            else if (!await WebRequest.Download(lib.Downloads.Artifact.URL, downloadPath))
            {
                return false;
            }
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
            if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == asset.Hash)
            {
                return true;
            }
            else if (!await WebRequest.Download(url, downloadPath))
            {
                return false;
            }
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
}
