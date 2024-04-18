using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Minecraft;

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
                MinecraftDownloadService.Init(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.LauncherSettings,
                    settings.MinecraftUrls
                );
                await MinecraftDownloadService.Download(useLocalVersionManifest: true);
            }
        );
    }
}
