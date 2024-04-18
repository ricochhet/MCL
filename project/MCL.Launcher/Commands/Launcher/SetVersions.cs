using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Minecraft;

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
