using System;
using System.IO;
using System.Linq;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Analyzers;

public static class NamespaceAnalyzer
{
    public static void Analyze(string filepath)
    {
        string[] files = VFS.GetFiles(filepath, "*.cs", SearchOption.AllDirectories);
        int success = 0;
        int fail = 0;
        foreach (string file in files)
        {
            string[] lines = VFS.ReadAllLines(file);
            string name = Search(lines);

            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
                continue;
            if (string.IsNullOrWhiteSpace(name))
            {
                NotificationService.Add(new Notification(NativeLogLevel.Error, "analyzer.error.namespace", [file, name ?? string.Empty]));
                continue;
            }

            string path = name.Replace("\\", "/").Replace(";", string.Empty).Replace(".", "/");
            string directory = VFS.GetDirectoryName(file).Replace("\\", "/").Replace("../", string.Empty).Replace(".", "/");
            if (path == directory)
                success++;
            else
            {
                fail++;
                NotificationService.Add(new Notification(NativeLogLevel.Error, "analyzer.error.namespace", [VFS.GetFileName(file), name]));
            }
        }

        NotificationService.Add(new Notification(NativeLogLevel.Info, "analyzer.output", [nameof(NamespaceAnalyzer), success.ToString(), fail.ToString(), files.Length.ToString()]));
    }

    private static string Search(string[] lines)
    {
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("namespace"))
            {
                string[] parts = trimmedLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return parts[1].TrimEnd('{');
            }
        }

        return string.Empty;
    }
}