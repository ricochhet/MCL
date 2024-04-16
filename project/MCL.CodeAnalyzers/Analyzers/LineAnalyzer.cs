using System.IO;
using System.Text.RegularExpressions;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class LineAnalyzer
{
    public static void Analyze(string filepath)
    {
        string[] files = VFS.GetFiles(filepath, "*.cs", SearchOption.AllDirectories);
        int total = 0;
        foreach (string file in files)
        {
            if (file.Contains("AssemblyInfo") || file.Contains("AssemblyAttributes"))
                continue;
            string[] lines = VFS.ReadAllLines(file);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    total++;
            }
        }

        NotificationService.Add(
            new(NativeLogLevel.Info, "analyzer.line.output", [nameof(LineAnalyzer), total.ToString()])
        );
    }

    [GeneratedRegex(@"(?<=namespace).*?;")]
    private static partial Regex NamespaceRegex();
}
