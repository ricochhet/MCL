using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.ModLoaders.Quilt;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadQuiltLoader : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-loader",
            async () =>
            {
                QuiltLoaderDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
                await QuiltLoaderDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
