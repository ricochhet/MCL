using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Launcher;

public class SetVersions : ILauncherCommand
{
    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--versions",
            async (string value) =>
            {
                await VersionManagerService.Init(settings, value);
            }
        );
    }
}
