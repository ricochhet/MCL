using System.IO;
using System.Text.RegularExpressions;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class NamespaceAnalyzer
{
    public static void Analyze(string filepath)
    {
        string[] files = VFS.GetFiles(filepath, "*.cs", SearchOption.AllDirectories);
        int success = 0;
        int fail = 0;
        foreach (string file in files)
        {
            Regex matchNamespace = NamespaceRegex();
            Match namespaceMatch = matchNamespace.Match(VFS.ReadAllText(file));
            string name = namespaceMatch.Value;

            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
            {
                success++;
                continue;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                NotificationService.Add(
                    new(NativeLogLevel.Error, "analyzer.error.namespace", [file, name ?? string.Empty])
                );
                continue;
            }

            string path = name.Replace("\\", "/")
                .Replace(";", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);
            string directory = VFS.GetDirectoryName(file)
                .Replace("\\", "/")
                .Replace("../", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);
            if (path == directory)
                success++;
            else
            {
                fail++;
                NotificationService.Add(
                    new(NativeLogLevel.Error, "analyzer.error.namespace", [VFS.GetFileName(file), name])
                );
            }
        }

        NotificationService.Add(
            new(
                NativeLogLevel.Info,
                "analyzer.output",
                [nameof(NamespaceAnalyzer), success.ToString(), fail.ToString(), files.Length.ToString()]
            )
        );
    }

    [GeneratedRegex(@"(?<=namespace).*?;")]
    private static partial Regex NamespaceRegex();
}
