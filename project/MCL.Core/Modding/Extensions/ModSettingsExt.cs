using System.Collections.Generic;
using System.Linq;
using MCL.Core.Launcher.Models;
using MCL.Core.Modding.Models;

namespace MCL.Core.Modding.Extensions;

public static class ModSettingsExt
{
    public static ModSettings Concat(this ModSettings modSettings, ModSettings concat)
    {
        List<string> copyOnlyTypes = modSettings
            .CopyOnlyTypes.Concat(concat.CopyOnlyTypes)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> unzipAndCopyTypes = modSettings
            .UnzipAndCopyTypes.Concat(concat.UnzipAndCopyTypes)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> modStores = modSettings
            .ModStores.Concat(concat.ModStores)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> deployPaths = modSettings
            .DeployPaths.Concat(concat.DeployPaths)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        return new ModSettings
        {
            CopyOnlyTypes = copyOnlyTypes,
            UnzipAndCopyTypes = unzipAndCopyTypes,
            ModStores = modStores,
            DeployPaths = deployPaths
        };
    }
}
