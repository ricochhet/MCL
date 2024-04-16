using System.Threading.Tasks;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.MinecraftQuilt;

namespace MCL.Launcher.Commands;

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
                if (!await QuiltInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    QuiltInstallerLaunchArgsHelper.Default(
                        config.LauncherPath,
                        config.LauncherVersion,
                        FabricInstallerType.CLIENT
                    ),
                    config.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
