/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Linq;
using MCL.Core.MiniCommon;
using MCL.Core.Modding.Models;

namespace MCL.Core.Modding.Extensions;

public static class ModSettingsExt
{
    /// <summary>
    /// Combines two ModSettings objects and keeps the last occurences of each property.
    /// </summary>
    public static ModSettings Concat(this ModSettings modSettings, ModSettings? concat)
    {
        List<string> copyOnlyTypes = modSettings
            .CopyOnlyTypes.Concat(concat?.CopyOnlyTypes ?? ValidationShims.ListEmpty<string>())
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> unzipAndCopyTypes = modSettings
            .UnzipAndCopyTypes.Concat(concat?.UnzipAndCopyTypes ?? ValidationShims.ListEmpty<string>())
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> modStores = modSettings
            .ModStores.Concat(concat?.ModStores ?? ValidationShims.ListEmpty<string>())
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        List<string> deployPaths = modSettings
            .DeployPaths.Concat(concat?.DeployPaths ?? ValidationShims.ListEmpty<string>())
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
