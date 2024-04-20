using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Servers.Paper.Helpers;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchPaperServer : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
    {
        CommandLine.ProcessArgument(
            args,
            "--launch-paper",
            options =>
            {
                if (
                    options.TryGetValue("gameversion", out string version)
                    && settings.LauncherInstance.Versions.Contains(version)
                )
                    settings.LauncherVersion.Version = version;

                if (
                    options.TryGetValue("paperversion", out string paperVersion)
                    && settings.LauncherInstance.PaperServerVersions.Contains(paperVersion)
                )
                    settings.LauncherVersion.PaperServerVersion = paperVersion;

                PaperLauncher.Launch(settings);
            }
        );

        return Task.CompletedTask;
    }
}
