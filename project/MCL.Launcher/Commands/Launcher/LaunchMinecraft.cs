using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchMinecraft : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
    {
        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MinecraftLauncher.Launch(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.LauncherSettings,
                    settings.LauncherUsername,
                    settings
                );
            }
        );

        return Task.CompletedTask;
    }
}
