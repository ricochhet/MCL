using System.Threading.Tasks;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.MinecraftFabric;

namespace MCL.Launcher.Commands;

public class DownloadFabricInstaller : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                FabricInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.FabricUrls);
                if (!await FabricInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    FabricInstallerLaunchArgsHelper.Default(
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
