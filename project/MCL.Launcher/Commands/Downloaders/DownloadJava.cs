using System.Threading.Tasks;
using MCL.Core.Java.Helpers;
using MCL.Core.Java.Services;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadJava : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings, Instance instance)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async _ =>
            {
                JavaDownloadService.Init(
                    settings.LauncherPath,
                    settings.MUrls,
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
