using System.IO;
using System.Threading.Tasks;
using MCL.CodeAnalyzers.Analyzers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Commands;

public class AnalyzeCode : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
    {
        CommandLine.ProcessArgument(
            args,
            "--analyze",
            (string value) =>
            {
                string[] files = VFS.GetFiles(value, "*.cs", SearchOption.AllDirectories);

                LineAnalyzer.Analyze(files);
                NamespaceAnalyzer.Analyze(files);
                LocalizationKeyAnalyzer.Analyze(files, LocalizationService.Localization);
            }
        );

        return Task.CompletedTask;
    }
}
