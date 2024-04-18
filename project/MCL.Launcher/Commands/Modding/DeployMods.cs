using System.Threading.Tasks;
using MCL.Core.Extensions.Launcher;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Modding;
using MCL.Core.Services.Modding;

namespace MCL.Launcher.Commands.Modding;

public class DeployMods : ILauncherCommand
{
    public Task Init(string[] args, Config config)
    {
        CommandLine.ProcessArgument(
            args,
            "--deploy-mods",
            (string value) =>
            {
                ModdingService.Save(value);
                ModdingService.Deploy(ModdingService.Load(value), ModPathResolver.ModDeployPath(config.LauncherPath));
                config.Save(ModdingService.ModConfig);
            }
        );

        return Task.CompletedTask;
    }
}
