using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadMinecraft : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings, Instance instance)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async options =>
            {
                _launcherVersion.Version = options.GetValueOrDefault("gameversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (!_launcherVersion.VersionExists())
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;

                MDownloadService.Init(
                    instance,
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.LauncherSettings,
                    settings.MUrls
                );
                await MDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
