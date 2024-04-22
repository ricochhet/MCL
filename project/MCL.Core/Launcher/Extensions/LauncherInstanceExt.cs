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
using MCL.Core.Launcher.Models;

namespace MCL.Core.Launcher.Extensions;

public static class LauncherInstanceExt
{
    public static LauncherInstance Concat(this LauncherInstance launcherInstance, LauncherInstance concat)
    {
        List<LauncherLoader> versions = launcherInstance
            .Versions.Concat(concat.Versions)
            .GroupBy(arg => arg.Version)
            .Select(group => group.Last())
            .ToList();

        List<LauncherLoader> fabricLoaders = launcherInstance
            .FabricLoaders.Concat(concat.FabricLoaders)
            .GroupBy(arg => arg.Version)
            .Select(group => group.Last())
            .ToList();

        List<LauncherLoader> quiltLoaders = launcherInstance
            .QuiltLoaders.Concat(concat.QuiltLoaders)
            .GroupBy(arg => arg.Version)
            .Select(group => group.Last())
            .ToList();

        List<string> paperServerVersions = launcherInstance
            .PaperServerVersions.Concat(concat.PaperServerVersions)
            .GroupBy(arg => arg)
            .Select(group => group.Last())
            .ToList();

        return new LauncherInstance
        {
            Versions = versions,
            FabricLoaders = fabricLoaders,
            QuiltLoaders = quiltLoaders,
            PaperServerVersions = paperServerVersions
        };
    }
}
