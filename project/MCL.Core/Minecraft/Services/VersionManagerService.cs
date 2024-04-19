using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.Servers.Paper.Helpers;

namespace MCL.Core.Minecraft.Services;

public static class VersionManagerService
{
    public static async Task<bool> SetVersions(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;
        if (!await VersionHelper.SetVersions(settings, launcherVersion, updateVersionManifest))
            return false;
        await FabricVersionHelper.SetVersions(settings, launcherVersion, updateVersionManifest);
        await QuiltVersionHelper.SetVersions(settings, launcherVersion, updateVersionManifest);
        await PaperVersionHelper.SetVersions(settings, launcherVersion, updateVersionManifest);
        return true;
    }
}
