using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadMinecraft : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MDownloadService.Init(
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
