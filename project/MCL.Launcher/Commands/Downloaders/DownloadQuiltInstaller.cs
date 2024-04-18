using System.Threading.Tasks;
using MCL.Core.Enums.MinecraftQuilt;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.MinecraftQuilt;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadQuiltInstaller : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-installer",
            async () =>
            {
                QuiltInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.QuiltUrls);
                if (!await QuiltInstallerDownloadService.Download(useLocalVersionManifest: true))
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    QuiltInstallerLaunchArgsHelper.Default(
                        config.LauncherPath,
                        config.LauncherVersion,
                        QuiltInstallerType.CLIENT
                    ),
                    config.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
