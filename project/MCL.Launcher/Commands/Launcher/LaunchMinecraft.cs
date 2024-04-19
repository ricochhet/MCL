using System.Threading.Tasks;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchMinecraft : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
    {
        CommandLine.ProcessArgument(
            args,
            "--launch",
            (string value) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "vanilla":
                            settings.LauncherSettings.ClientType = ClientType.VANILLA;
                            break;
                        case "fabric":
                            settings.LauncherSettings.ClientType = ClientType.FABRIC;
                            break;
                        case "quilt":
                            settings.LauncherSettings.ClientType = ClientType.QUILT;
                            break;
                    }
                }

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
