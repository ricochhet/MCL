using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Resolvers;
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
            options =>
            {
                settings.LauncherSettings.ClientType = GenericEnumParser.Parse(
                    options.GetValueOrDefault("client"),
                    ClientType.VANILLA
                );

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