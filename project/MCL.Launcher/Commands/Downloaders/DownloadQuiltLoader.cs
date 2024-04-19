using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadQuiltLoader : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-loader",
            async options =>
            {
                _launcherVersion.Version = options.GetValueOrDefault("gameversion") ?? "latest";
                _launcherVersion.QuiltLoaderVersion = options.GetValueOrDefault("loaderversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (!_launcherVersion.VersionExists() || !_launcherVersion.QuiltLoaderVersionExists())
                    return;
                if (!await QuiltVersionHelper.SetLoaderVersion(settings, _launcherVersion, update))
                    return;

                QuiltLoaderDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
                await QuiltLoaderDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
