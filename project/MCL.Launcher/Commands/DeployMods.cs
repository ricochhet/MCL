using System.Threading.Tasks;
using MCL.Core.Extensions;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Modding;

namespace MCL.Launcher.Commands;

public class DeployMods : ILauncherCommand
{
    public Task Init(string[] args, Config config)
    {
        CommandLine.ProcessArgument(
            args,
            "--mods",
            () =>
            {
                ModdingService.Save("fabric-mods");
                ModdingService.Deploy(
                    ModdingService.Load("fabric-mods"),
                    VFS.FromCwd(config.LauncherPath.Path, "mods")
                );
                config.Save(ModdingService.ModConfig);
            }
        );

        return Task.CompletedTask;
    }
}
