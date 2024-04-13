using System;
using System.Collections.Generic;
using System.IO;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Analyzers;

public static class LocalizationKeyAnalyzer
{
    public static void Analyze(string filepath, Localization localization)
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
            string[] lines = VFS.ReadAllLines(file);
            string name = Search(lines).Replace("\"", string.Empty);

            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
                continue;
            if (string.IsNullOrWhiteSpace(name))
                continue;
            if (!localization.Entries.ContainsKey(name))
            {
                fail++;
                NotificationService.Add(new Notification(NativeLogLevel.Error, "analyzer.error.localization", [file, name]));
            }
            else
                success++;
        }

        NotificationService.Add(new Notification(NativeLogLevel.Info, "analyzer.output", [nameof(LocalizationKeyAnalyzer), success.ToString(), fail.ToString(), files.Length.ToString()]));
    }

    private static string Search(string[] lines)
    {
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("new Notification(NativeLogLevel"))
            {
                string[] parts = trimmedLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return parts[2].TrimEnd(',');
            }
        }

        return string.Empty;
    }
}