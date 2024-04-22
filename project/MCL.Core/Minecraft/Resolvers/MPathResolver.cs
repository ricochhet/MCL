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
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Resolvers;

public static class MPathResolver
{
    public static string ClientLibrary(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}.jar").Replace("\\", "/");

    public static string Libraries(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}-natives").Replace("\\", "/");

    public static string AssetsPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "assets");

    public static string LibraryPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "libraries");

    public static string ServerPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "server");

    public static string VersionPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(launcherPath.Path, "versions", versionDetails.ID);

    public static string ClientJarPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), versionDetails.ID + ".jar");

    public static string ClientMappingsPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.txt");

    public static string ClientIndexPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(AssetsPath(launcherPath), "indexes", versionDetails.Assets + ".json");

    public static string LoggingPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.xml");

    public static string LoggingPath(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, "client.xml");

    public static string ServerJarPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.jar");

    public static string ServerMappingsPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.txt");

    public static string ServerEulaPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string ServerPropertiesPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string VersionManifestPath(LauncherPath launcherPath) =>
        VFS.Combine(launcherPath.Path, "version_manifest.json");

    public static string VersionDetailsPath(LauncherPath launcherPath, MVersion version) =>
        VFS.Combine(launcherPath.Path, "versions", version.ID + ".json");
}
