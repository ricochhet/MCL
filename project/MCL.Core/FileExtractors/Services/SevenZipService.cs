using System;
using MCL.Core.FileExtractors.Models;
using MCL.Core.FileExtractors.Resolvers;
using MCL.Core.MiniCommon.Helpers;

namespace MCL.Core.FileExtractors.Services;

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
            string.Format(SevenZipSettings.ExtractArguments, source, destination),
            Environment.CurrentDirectory,
            false
        );
    }
}
