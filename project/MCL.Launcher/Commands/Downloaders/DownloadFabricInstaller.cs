using System.Threading.Tasks;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.ModLoaders.Fabric;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.ModLoaders.Fabric;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadFabricInstaller : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                FabricInstallerDownloadService.Init(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.FabricUrls
                );
                if (!await FabricInstallerDownloadService.Download(useLocalVersionManifest: true))
                    return;

                JavaLauncher.Launch(
                    settings,
                    FabricInstallerArgs.DefaultJvmArguments(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        FabricInstallerType.CLIENT
                    ),
                    settings.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
