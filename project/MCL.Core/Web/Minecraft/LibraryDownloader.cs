using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;

namespace MCL.Core.Web.Minecraft;

public static class LibraryDownloader
{
    public static async Task<bool> Download(
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
}