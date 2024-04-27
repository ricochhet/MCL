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

using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.FileSystem;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Java.Resolvers;

public static class JavaPathResolver
{
    /// <summary>
    /// The Java runtime path.
    /// </summary>
    public static string JavaRuntimePath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? ValidationShims.StringEmpty(), "runtime");

    /// <summary>
    /// The Java runtime 'home' specified by the working directory.
    /// </summary>
    public static string JavaRuntimeHome(string workingDirectory, JavaRuntimeType? javaRuntimeType) =>
        VFS.Combine(workingDirectory, "runtime", JavaRuntimeTypeResolver.ToString(javaRuntimeType));

    /// <summary>
    /// The Java runtime 'bin' specified by the working directory.
    /// </summary>
    public static string JavaRuntimeBin(string workingDirectory) => VFS.Combine(workingDirectory, "bin");

    /// <summary>
    /// The Java runtime version manifest path.
    /// </summary>
    public static string JavaVersionManifestPath(LauncherPath? launcherPath) =>
        VFS.Combine(JavaRuntimePath(launcherPath), "version_details.json");

    /// <summary>
    /// The Java runtime version details path.
    /// </summary>
    public static string JavaVersionDetailsPath(LauncherPath? launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, $"{javaRuntimeVersion}.json");

    /// <summary>
    /// The Java runtime version path.
    /// </summary>
    public static string JavaRuntimeVersionPath(LauncherPath? launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
}
