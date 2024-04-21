using System.Text.RegularExpressions;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class NamespaceAnalyzer
{
    public static void Analyze(string[] files)
    {
        int success = 0;
        int fail = 0;
        foreach (string file in files)
        {
            string fileData = VFS.ReadAllText(file);
            Regex matchNamespace = NamespaceRegex();
            Match namespaceMatch = matchNamespace.Match(fileData);
            string name = namespaceMatch.Value;

            if (
                file.Contains("AssemblyInfo")
                || file.Contains("AssemblyAttributes")
                || file.Contains("GlobalSuppressions")
            )
            {
                success++;
                continue;
            }

            if (ObjectValidator<string>.IsNullOrWhiteSpace([name], NativeLogLevel.Debug))
            {
                NotificationService.Log(NativeLogLevel.Error, "analyzer.error.namespace", file, name ?? string.Empty);
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
                string oldNamespace = "namespace " + path.Replace("/", ".") + ";";
                string newNamespace = "namespace " + directory.Replace("/", ".") + ";";
                VFS.WriteFile(file, fileData.Replace(oldNamespace, newNamespace));

                fail++;
                NotificationService.Log(NativeLogLevel.Error, "analyzer.error.namespace", VFS.GetFileName(file), name);
            }
        }

        NotificationService.Log(
            NativeLogLevel.Info,
            "analyzer.output",
            nameof(NamespaceAnalyzer),
            success.ToString(),
            fail.ToString(),
            files.Length.ToString()
        );
    }

    [GeneratedRegex(@"(?<=namespace).*?;")]
    private static partial Regex NamespaceRegex();
}
