using System.Threading.Tasks;
using MCL.Core.Helpers.Java;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Java;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadJava : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadService.Init(
                    config.LauncherPath,
                    config.MinecraftUrls,
                    JavaVersionHelper.GetDownloadedMCVersionJava(
                        config.LauncherPath,
                        config.LauncherVersion,
                        config.LauncherSettings
                    ),
                    config.LauncherSettings.JavaRuntimePlatform
                );

                await JavaDownloadService.Download();
            }
        );
    }
}
