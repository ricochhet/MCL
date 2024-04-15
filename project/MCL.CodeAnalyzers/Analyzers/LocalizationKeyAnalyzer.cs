using System.IO;
using System.Text.RegularExpressions;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class LocalizationKeyAnalyzer
{
    public static void Analyze(string filepath, Localization localization)
    {
        string[] files = VFS.GetFiles(filepath, "*.cs", SearchOption.AllDirectories);
        int success = 0;
        int fail = 0;

        if (localization?.Entries == null || localization.Entries?.Count <= 0)
            return;

        foreach (string file in files)
        {
            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
                continue;

            Regex matchNotification = NotificationServiceRegex();
            MatchCollection notificationMatches = matchNotification.Matches(VFS.ReadAllText(file));

            foreach (Match match in notificationMatches)
            {
                Regex matchQuotes = QuoteRegex();
                Match quoteMatch = matchQuotes.Match(match.Value);

                if (string.IsNullOrWhiteSpace(quoteMatch.Value))
                    continue;

                if (!localization.Entries.ContainsKey(quoteMatch.Value.Replace("\"", string.Empty)))
                {
                    fail++;
                    NotificationService.Add(
                        new(NativeLogLevel.Error, "analyzer.error.localization", [file, quoteMatch.Value])
                    );
                }
                else
                {
                    success++;
                }
            }
        }

        NotificationService.Add(
            new(
                NativeLogLevel.Info,
                "analyzer.output",
                [nameof(LocalizationKeyAnalyzer), success.ToString(), fail.ToString(), files.Length.ToString()]
            )
        );
    }

    [GeneratedRegex(@"NotificationService\.Add.*?\);")]
    private static partial Regex NotificationServiceRegex();

    [GeneratedRegex("\"([^\"]*)\"")]
    private static partial Regex QuoteRegex();
}
