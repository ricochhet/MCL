using System.Threading.Tasks;
using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Quilt.Enums;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadQuiltInstaller : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-installer",
            async () =>
            {
                QuiltInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
                if (!await QuiltInstallerDownloadService.Download(useLocalVersionManifest: true))
                    return;

                JavaLauncher.Launch(
                    settings,
                    QuiltInstallerArgs.DefaultJvmArguments(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        QuiltInstallerType.CLIENT
                    ),
                    settings.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
