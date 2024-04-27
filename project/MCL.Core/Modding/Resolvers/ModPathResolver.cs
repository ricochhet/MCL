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

using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.FileSystem;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Modding.Resolvers;

public static class ModPathResolver
{
    /// <summary>
    /// The mod store path.
    /// </summary>
    public static string ModPath(LauncherPath? launcherPath, string? modStoreName) =>
        VFS.FromCwd(
            launcherPath?.ModPath ?? ValidationShims.StringEmpty(),
            modStoreName ?? ValidationShims.StringEmpty()
        );

    /// <summary>
    /// The mod store data path.
    /// </summary>
    public static string ModStorePath(LauncherPath? launcherPath, string? modStoreName) =>
        VFS.FromCwd(launcherPath?.ModPath ?? ValidationShims.StringEmpty(), $"{modStoreName}.modstore.json");

    /// <summary>
    /// The mod deployment path.
    /// </summary>
    public static string ModDeployPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? ValidationShims.StringEmpty(), "mods");
}
