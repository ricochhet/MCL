using System.Threading.Tasks;
using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Services;

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
