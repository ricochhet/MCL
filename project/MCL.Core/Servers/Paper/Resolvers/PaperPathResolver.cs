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
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Validation.Operators;

namespace MCL.Core.Servers.Paper.Resolvers;

public static class PaperPathResolver
{
    /// <summary>
    /// The Paper server installation path.
    /// </summary>
    public static string InstallerPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.PaperPath ?? StringOperator.Empty(),
            launcherVersion?.MVersion ?? StringOperator.Empty()
        );

    /// <summary>
    /// The Paper server version manifest path.
    /// </summary>
    public static string VersionManifestPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.PaperPath ?? StringOperator.Empty(),
            launcherVersion?.MVersion ?? StringOperator.Empty(),
            "paper_manifest.json"
        );

    /// <summary>
    /// The Paper server client jar name.
    /// </summary>
    public static string JarName(LauncherVersion? launcherVersion) =>
        $"paper-{launcherVersion?.MVersion}-{launcherVersion?.PaperServerVersion}.jar";

    /// <summary>
    /// The Paper server client jar path.
    /// </summary>
    public static string JarPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(InstallerPath(launcherPath, launcherVersion), JarName(launcherVersion));
}
