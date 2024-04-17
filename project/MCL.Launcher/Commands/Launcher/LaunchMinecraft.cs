using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchMinecraft : ILauncherCommand
{
    public Task Init(string[] args, Config config)
    {
        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MinecraftLaunchHelper.Launch(
                    config.LauncherPath,
                    config.LauncherVersion,
                    config.LauncherSettings,
                    config.LauncherUsername,
                    config
                );
            }
        );

        return Task.CompletedTask;
    }
}
