using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Modding.Resolvers;
using MCL.Core.Modding.Services;

namespace MCL.Launcher.Commands.Modding;

public class DeployMods : ILauncherCommand
{
    public Task Init(string[] args, Settings settings, Instance instance)
    {
        CommandLine.ProcessArgument(
            args,
            "--deploy-mods",
            (string value) =>
            {
                ModdingService.Save(value);
                ModdingService.Deploy(ModdingService.Load(value), ModPathResolver.ModDeployPath(settings.LauncherPath));
                settings.Save(ModdingService.ModSettings);
            }
        );

        return Task.CompletedTask;
    }
}
