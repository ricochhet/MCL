using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadPaperServer : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-paper-server",
            async () =>
            {
                PaperServerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.PaperUrls);
                await PaperServerDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
