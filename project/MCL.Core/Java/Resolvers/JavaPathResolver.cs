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
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Resolvers;

public static class JavaPathResolver
{
    public static string JavaRuntimePath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "runtime");

    public static string JavaRuntimeHome(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(workingDirectory, "runtime", JavaRuntimeTypeResolver.ToString(javaRuntimeType));

    public static string JavaRuntimeBin(string workingDirectory, JavaRuntimeType javaRuntimeType) =>
        VFS.Combine(JavaRuntimeHome(workingDirectory, javaRuntimeType), "bin");

    public static string JavaRuntimeBin(string workingDirectory) => VFS.Combine(workingDirectory, "bin");

    public static string DownloadedJavaVersionManifestPath(LauncherPath launcherPath) =>
        VFS.Combine(JavaRuntimePath(launcherPath), "version_details.json");

    public static string DownloadedJavaVersionDetailsPath(LauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion, $"{javaRuntimeVersion}.json");

    public static string DownloadedJavaRuntimePath(LauncherPath launcherPath, string javaRuntimeVersion) =>
        VFS.Combine(JavaRuntimePath(launcherPath), javaRuntimeVersion);
}
