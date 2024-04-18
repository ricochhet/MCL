using System;
using MCL.Core.Helpers;
using MCL.Core.Models.SevenZip;
using MCL.Core.Resolvers.SevenZip;

namespace MCL.Core.Services.SevenZip;

public static class SevenZipService
{
    private static SevenZipSettings SevenZipSettings { get; set; }

    public static void Init(SevenZipSettings sevenZipSettings)
    {
        SevenZipSettings = sevenZipSettings;
    }

    public static void Extract(string source, string destination)
    {
        ProcessHelper.RunProcess(
            SevenZipPathResolver.SevenZipPath(SevenZipSettings),
            string.Format(SevenZipSettings.SevenZipExtractArgs, source, destination),
            Environment.CurrentDirectory,
            false
        );
    }
}
