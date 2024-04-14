using System.IO;
using System.Text.RegularExpressions;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class LocalizationKeyAnalyzer
{
#pragma warning disable IDE0079
#pragma warning disable S3776 // (Reduce cognitive complexity) TODO: Evaluate refactor
    public static void Analyze(string filepath, Localization localization)
#pragma warning restore
    {
        string[] files = VFS.GetFiles(filepath, "*.cs", SearchOption.AllDirectories);
        int success = 0;
        int fail = 0;
        if (localization?.Entries == null)
            return;
        if (localization.Entries?.Count <= 0)
            return;
        foreach (string file in files)
        {
            Regex matchNotification = NotificationServiceRegex();
            MatchCollection notificationMatches = matchNotification.Matches(VFS.ReadAllText(file));

            for (int count = 0; count < notificationMatches.Count; count++)
            {
                Regex matchQuotes = QuoteRegex();
                Match quoteMatch = matchQuotes.Match(notificationMatches[count].Value);
                string name = quoteMatch.Value;
                if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
                    continue;
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                if (!localization.Entries.ContainsKey(name.Replace("\"", string.Empty)))
                {
                    fail++;
                    NotificationService.Add(new(NativeLogLevel.Error, "analyzer.error.localization", [file, name]));
                }
                else
                    success++;
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
