using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadPaperServer : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-paper-server",
            async options =>
            {
                _launcherVersion.Version = options.GetValueOrDefault("gameversion") ?? "latest";
                _launcherVersion.PaperServerVersion = options.GetValueOrDefault("paperversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (
                    ObjectValidator<string>.IsNullOrWhitespace(
                        _launcherVersion.Version,
                        _launcherVersion.PaperServerVersion
                    )
                )
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;
                if (!await PaperVersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;

                PaperServerDownloadService.Init(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.LauncherInstance,
                    settings.PaperUrls
                );
                await PaperServerDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
