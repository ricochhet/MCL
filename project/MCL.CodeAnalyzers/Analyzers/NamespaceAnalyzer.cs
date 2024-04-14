using System.IO;
using MCL.Core.Helpers;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Analyzers;

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
            string name = StringHelper.Search(lines, "namespace", ';', 1);

            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
            {
                success++;
                continue;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                NotificationService.Add(
                    new Notification(NativeLogLevel.Error, "analyzer.error.namespace", [file, name ?? string.Empty])
                );
                continue;
            }

            string path = name.Replace("\\", "/").Replace(";", string.Empty).Replace(".", "/");
            string directory = VFS.GetDirectoryName(file)
                .Replace("\\", "/")
                .Replace("../", string.Empty)
                .Replace(".", "/");
            if (path == directory)
                success++;
            else
            {
                fail++;
                NotificationService.Add(
                    new Notification(NativeLogLevel.Error, "analyzer.error.namespace", [VFS.GetFileName(file), name])
                );
            }
        }

        NotificationService.Add(
            new Notification(
                NativeLogLevel.Info,
                "analyzer.output",
                [nameof(NamespaceAnalyzer), success.ToString(), fail.ToString(), files.Length.ToString()]
            )
        );
    }
}
