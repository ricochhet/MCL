using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.ModLoaders.Fabric;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadFabricLoader : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async () =>
            {
                FabricLoaderDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.FabricUrls);
                await FabricLoaderDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
