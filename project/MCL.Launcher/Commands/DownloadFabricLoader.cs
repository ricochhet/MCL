using System.Threading.Tasks;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.MinecraftFabric;

namespace MCL.Launcher.Commands;

public class DownloadFabricLoader : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async () =>
            {
                FabricLoaderDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.FabricUrls);
                await FabricLoaderDownloadService.Download();
            }
        );
    }
}
