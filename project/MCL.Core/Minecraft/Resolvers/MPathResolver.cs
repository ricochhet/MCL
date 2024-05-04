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
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MCL.Core.Minecraft.Resolvers;

public static class MPathResolver
{
    public const string BaseVersionsPath = "versions";
    public const string BaseAssetsPath = "assets";
    public const string BaseLibrariesPath = "libraries";
    public const string BaseServerPath = "server";

    /// <summary>
    /// The main client library jar.
    /// </summary>
    public static string ClientLibrary(LauncherVersion? launcherVersion) =>
        VFS.Combine(
            BaseVersionsPath,
            launcherVersion?.MVersion ?? Validate.For.EmptyString(),
            $"{launcherVersion?.MVersion}.jar"
        );

    /// <summary>
    /// The natives library path specified by the MVersion.
    /// </summary>
    public static string NativesLibraries(LauncherVersion? launcherVersion) =>
        VFS.Combine(
            BaseVersionsPath,
            launcherVersion?.MVersion ?? Validate.For.EmptyString(),
            $"{launcherVersion?.MVersion}-natives"
        );

    /// <summary>
    /// The assets path.
    /// </summary>
    public static string AssetsPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? Validate.For.EmptyString(), BaseAssetsPath);

    /// <summary>
    /// The library path.
    /// </summary>
    public static string LibraryPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? Validate.For.EmptyString(), BaseLibrariesPath);

    /// <summary>
    /// The server client path.
    /// </summary>
    public static string ServerPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? Validate.For.EmptyString(), BaseServerPath);

    /// <summary>
    /// The client version path specified by the MVersionDetails identifier.
    /// </summary>
    public static string VersionPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(
            launcherPath?.MPath ?? Validate.For.EmptyString(),
            BaseVersionsPath,
            versionDetails?.ID ?? Validate.For.EmptyString()
        );

    /// <summary>
    /// The main client library jar path.
    /// </summary>
    public static string ClientJarPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), versionDetails?.ID + ".jar");

    /// <summary>
    /// The client mappings path specified by the MVersionDetails identifier.
    /// </summary>
    public static string ClientMappingsPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.txt");

    /// <summary>
    /// The client asset index path.
    /// </summary>
    public static string ClientAssetIndexPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(AssetsPath(launcherPath), "indexes", versionDetails?.Assets + ".json");

    /// <summary>
    /// The logging configuration path.
    /// </summary>
    public static string LoggingPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.xml");

    // The logging configuration XML file.
    public static string LoggingPath(LauncherVersion? launcherVersion) =>
        VFS.Combine(BaseVersionsPath, launcherVersion?.MVersion ?? Validate.For.EmptyString(), "client.xml");

    /// <summary>
    /// The server library jar path.
    /// </summary>
    public static string ServerJarPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails?.ID}.jar");

    /// <summary>
    /// The server mappings path specified by the MVersionDetails identifier.
    /// </summary>
    public static string ServerMappingsPath(LauncherPath? launcherPath, MVersionDetails? versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails?.ID}.txt");

    /// <summary>
    /// The server Eula path.
    /// </summary>
    public static string ServerEulaPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    /// <summary>
    /// The server properties path.
    /// </summary>
    public static string ServerPropertiesPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    /// <summary>
    /// The main version manifest path.
    /// </summary>
    public static string VersionManifestPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.MPath ?? Validate.For.EmptyString(), "version_manifest.json");

    /// <summary>
    /// The version details path specified by the MVersion identifier.
    /// </summary>
    public static string VersionDetailsPath(LauncherPath? launcherPath, MVersion? version) =>
        VFS.Combine(launcherPath?.MPath ?? Validate.For.EmptyString(), BaseVersionsPath, version?.ID + ".json");
}
