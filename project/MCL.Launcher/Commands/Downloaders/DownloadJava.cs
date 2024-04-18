using System.Threading.Tasks;
using MCL.Core.Helpers.Java;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Java;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadJava : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadService.Init(
                    settings.LauncherPath,
                    settings.MinecraftUrls,
                    JavaVersionHelper.GetDownloadedMCVersionJava(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        settings.LauncherSettings
                    ),
                    settings.LauncherSettings.JavaRuntimePlatform
                );

                await JavaDownloadService.Download();
            }
        );
    }
}
