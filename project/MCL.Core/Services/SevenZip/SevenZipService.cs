using System;
using MCL.Core.Helpers;
using MCL.Core.Models.SevenZip;
using MCL.Core.Resolvers.SevenZip;

namespace MCL.Core.Services.SevenZip;

public static class SevenZipService
{
    private static SevenZipConfig SevenZipConfig { get; set; }

    public static void Init(SevenZipConfig sevenZipConfig)
    {
        SevenZipConfig = sevenZipConfig;
    }

    public static void Extract(string source, string destination)
    {
        ProcessHelper.RunProcess(
            SevenZipPathResolver.SevenZipPath(SevenZipConfig),
            string.Format(SevenZipConfig.SevenZipExtractArgs, source, destination),
            Environment.CurrentDirectory,
            false
        );
    }
}
