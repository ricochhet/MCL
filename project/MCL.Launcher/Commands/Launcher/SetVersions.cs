using System.Threading.Tasks;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Minecraft;

namespace MCL.Launcher.Commands.Launcher;

public class SetVersions : ILauncherCommand
{
    public async Task Init(string[] args, Config config)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--versions",
            async (string value) =>
            {
                await VersionManagerService.Init(config, value);
            }
        );
    }
}
