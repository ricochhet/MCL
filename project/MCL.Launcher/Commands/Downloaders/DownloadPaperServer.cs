using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Paper;

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
