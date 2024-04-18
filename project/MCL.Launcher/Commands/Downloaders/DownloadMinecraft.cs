using System.Threading.Tasks;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Minecraft;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadMinecraft : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MinecraftDownloadService.Init(
                    config.LauncherPath,
                    config.LauncherVersion,
                    config.LauncherSettings,
                    config.MinecraftUrls
                );
                await MinecraftDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
