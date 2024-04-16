using System.Threading.Tasks;
using MCL.CodeAnalyzers.Analyzers;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers.Commands;

public class AnalyzeCode : ILauncherCommand
{
    public Task Init(string[] args, Config config)
    {
        CommandLine.ProcessArgument(
            args,
            "--analyze",
            (string value) =>
            {
                LineAnalyzer.Analyze(value);
                NamespaceAnalyzer.Analyze(value);
                LocalizationKeyAnalyzer.Analyze(value, LocalizationService.Localization);
            }
        );

        return Task.CompletedTask;
    }
}
