using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Launcher;

public class SetVersions : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = new();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--versions",
            async options =>
            {
                _launcherVersion.Version = options.GetValueOrDefault("minecraft") ?? "latest";
                _launcherVersion.FabricInstallerVersion = options.GetValueOrDefault("fabric-installer") ?? "latest";
                _launcherVersion.FabricLoaderVersion = options.GetValueOrDefault("fabric-loader") ?? "latest";
                _launcherVersion.QuiltInstallerVersion = options.GetValueOrDefault("quilt-installer") ?? "latest";
                _launcherVersion.QuiltLoaderVersion = options.GetValueOrDefault("quilt-loader") ?? "latest";
                _launcherVersion.PaperServerVersion = options.GetValueOrDefault("paper") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool updateVersionManifest))
                    return;
                await VersionManagerService.SetVersions(settings, _launcherVersion, updateVersionManifest);
            }
        );
    }
}
