using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperLauncher
{
    public static void Launch(Settings settings)
    {
        if (ObjectValidator<string>.IsNullOrWhitespace(settings?.LauncherVersion?.Version))
            return;

        settings.Save(PaperServerArgs.DefaultJvmArguments(settings.LauncherPath, settings.LauncherVersion));
        JavaLauncher.Launch(
            settings,
            PaperPathResolver.InstallerPath(settings.LauncherPath, settings.LauncherVersion),
            settings.PaperJvmArguments,
            settings.LauncherSettings.JavaRuntimeType
        );
    }
}
