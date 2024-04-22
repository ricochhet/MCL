using System.Collections.Generic;
using System.Text.RegularExpressions;
using MCL.CodeAnalyzers.Analyzers.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class LocalizationKeyAnalyzer
{
    public static void Analyze(string[] files, Localization localization)
    {
        int success = 0;
        int fail = 0;

        if (ObjectValidator<Dictionary<string, string>>.IsNullOrEmpty(localization?.Entries))
            return;

        foreach (string file in files)
        {
            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;

            Regex matchNotification = NotificationServiceRegex();
            MatchCollection notificationMatches = matchNotification.Matches(VFS.ReadAllText(file));

            foreach (Match match in notificationMatches)
            {
                Regex matchQuotes = QuoteRegex();
                Match quoteMatch = matchQuotes.Match(match.Value);

                if (ObjectValidator<string>.IsNullOrWhiteSpace([quoteMatch?.Value], NativeLogLevel.Debug))
                    continue;

                if (!localization.Entries.ContainsKey(quoteMatch.Value.Replace("\"", string.Empty)))
                {
                    fail++;
                    NotificationService.Log(
                        NativeLogLevel.Error,
                        "analyzer.error.localization",
                        file,
                        quoteMatch.Value
                    );
                }
                else
                {
                    success++;
                }
            }
        }

        NotificationService.Log(
            NativeLogLevel.Info,
            "analyzer.output",
            nameof(LocalizationKeyAnalyzer),
            success.ToString(),
            fail.ToString(),
            (success + fail).ToString()
        );
    }

    [GeneratedRegex(@"NotificationService\.[^;]*\);(?=\s|$)")]
    private static partial Regex NotificationServiceRegex();

    [GeneratedRegex("\"([^\"]*)\"")]
    private static partial Regex QuoteRegex();
}
