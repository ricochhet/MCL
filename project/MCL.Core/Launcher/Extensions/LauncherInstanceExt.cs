using System.Collections.Generic;
using System.Linq;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Launcher.Extensions;

public static class LauncherInstanceExt
{
    public static LauncherInstance Concat(this LauncherInstance launcherInstance, LauncherInstance concat)
    {
        List<string> versions = launcherInstance
            .Versions.Concat(concat.Versions)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<LauncherModLoader> fabricLoaders = launcherInstance
            .FabricLoaders.Concat(concat.FabricLoaders)
            .GroupBy(arg => arg.Version)
            .Select(group => group.Last())
            .ToList();

        List<LauncherModLoader> quiltLoaders = launcherInstance
            .QuiltLoaders.Concat(concat.QuiltLoaders)
            .GroupBy(arg => arg.Version)
            .Select(group => group.Last())
            .ToList();

        return new LauncherInstance
        {
            Versions = versions,
            FabricLoaders = fabricLoaders,
            QuiltLoaders = quiltLoaders
        };
    }
}
