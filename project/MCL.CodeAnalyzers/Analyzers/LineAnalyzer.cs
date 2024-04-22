using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MCL.CodeAnalyzers.Analyzers.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class LineAnalyzer
{
    public static void Analyze(string[] files)
    {
        List<int> fileLines = [];
        foreach (string file in files)
        {
            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;
            string[] lines = VFS.ReadAllLines(file);
            int fileLineCount = 0;
            foreach (
                string line in lines.Where(a =>
                    ObjectValidator<string>.IsNotNullOrWhiteSpace([a], NativeLogLevel.Debug)
                )
            )
                fileLineCount++;

            fileLines.Add(fileLineCount);
        }

        NotificationService.Log(
            NativeLogLevel.Info,
            "analyzer.line.output",
            nameof(LineAnalyzer),
            fileLines.Sum().ToString(),
            Math.Round(fileLines.Average()).ToString()
        );
    }

    [GeneratedRegex(@"(?<=namespace).*?;")]
    private static partial Regex NamespaceRegex();
}
