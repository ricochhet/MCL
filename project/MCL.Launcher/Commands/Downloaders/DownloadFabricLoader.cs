using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadFabricLoader : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async _ =>
            {
                FabricLoaderDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.FabricUrls);
                await FabricLoaderDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
