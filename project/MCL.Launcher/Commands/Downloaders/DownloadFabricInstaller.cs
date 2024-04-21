using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadFabricInstaller : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async options =>
            {
                _launcherVersion.FabricInstallerVersion = options.GetValueOrDefault("installerversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (ObjectValidator<string>.IsNullOrWhiteSpace([_launcherVersion.FabricInstallerVersion]))
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;
                if (!await FabricVersionHelper.SetInstallerVersion(settings, _launcherVersion, update))
                    return;

                FabricInstallerDownloadService.Init(
                    settings.LauncherPath,
                    settings.LauncherVersion,
                    settings.FabricUrls
                );
                if (!await FabricInstallerDownloadService.Download(useLocalVersionManifest: true))
                    return;

                JavaLauncher.Launch(
                    settings,
                    FabricPathResolver.InstallersPath(settings.LauncherPath),
                    FabricInstallerArgs.DefaultJvmArguments(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        FabricInstallerType.INSTALL_CLIENT
                    ),
                    settings.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
