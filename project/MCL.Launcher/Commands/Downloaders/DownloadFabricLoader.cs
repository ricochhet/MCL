using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadFabricLoader : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async options =>
            {
                _launcherVersion.Version = options.GetValueOrDefault("gameversion") ?? "latest";
                _launcherVersion.FabricLoaderVersion = options.GetValueOrDefault("loaderversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (!_launcherVersion.VersionExists() || !_launcherVersion.FabricLoaderVersionExists())
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;
                if (!await FabricVersionHelper.SetLoaderVersion(settings, _launcherVersion, update))
                    return;

                FabricLoaderDownloadService.Init(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.LauncherInstance,
                    settings.FabricUrls
                );
                await FabricLoaderDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
