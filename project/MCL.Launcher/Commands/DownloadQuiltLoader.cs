using System.Threading.Tasks;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.MinecraftQuilt;

namespace MCL.Launcher.Commands;

public class DownloadQuiltLoader : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-loader",
            async () =>
            {
                QuiltLoaderDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.QuiltUrls);
                await QuiltLoaderDownloadService.Download();
            }
        );
    }
}
