using System.Threading.Tasks;
using MCL.Core.Extensions.Launcher;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Modding;
using MCL.Core.Services.Modding;

namespace MCL.Launcher.Commands.Modding;

public class DeployMods : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
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
