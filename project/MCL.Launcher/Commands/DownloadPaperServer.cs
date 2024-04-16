using System.Threading.Tasks;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Paper;

namespace MCL.Launcher.Commands;

public class DownloadPaperServer : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-paper-server",
            async () =>
            {
                PaperServerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.PaperUrls);
                await PaperServerDownloadService.Download();
            }
        );
    }
}
