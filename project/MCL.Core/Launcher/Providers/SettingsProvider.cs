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
using MiniCommon.BuildInfo;
using MiniCommon.IO;
using MiniCommon.Providers;

namespace MCL.Core.Launcher.Providers;

public static class SettingsProvider
{
    public const string SettingsFileName = "mcl.json";
    public const string SimpleMLaunchFileName = "launch.txt";
    public const string SimplePaperLaunchFileName = "paper.txt";
    public static readonly string SimpleMLaunchFilePath = VFS.FromCwd(
        AssemblyConstants.DataDirectory,
        SimpleMLaunchFileName
    );
    public static readonly string SimplePaperLaunchFilePath = VFS.FromCwd(
        AssemblyConstants.DataDirectory,
        SimplePaperLaunchFileName
    );
    private static readonly string _settingsFilePath = VFS.FromCwd(AssemblyConstants.DataDirectory, SettingsFileName);

    /// <summary>
    /// Initialize the Settings service.
    /// Create a new configuration file if one is not present.
    /// </summary>
    public static void FirstRun()
    {
        if (!VFS.Exists(_settingsFilePath))
        {
            NotificationProvider.Warn("launcher.settings.missing", SettingsFileName, AssemblyConstants.DataDirectory);
            NotificationProvider.Info("launcher.settings.setup", _settingsFilePath);
            Settings settings =
                new()
                {
                    MainClassNames = new(),
                    LauncherMemory = new(),
                    LauncherInstance = new(),
                    OverrideLauncherInstance = new(),
                    LauncherUsername = new(),
                    LauncherPath = new(),
                    LauncherVersion = new(),
                    LauncherSettings = new(),
                    MUrls = new(),
                    FabricUrls = new(),
                    QuiltUrls = new(),
                    PaperUrls = new(),
                    MJvmArguments = new(),
                    OverrideMJvmArguments = new(),
                    FabricJvmArguments = new(),
                    OverrideFabricJvmArguments = new(),
                    QuiltJvmArguments = new(),
                    OverrideQuiltJvmArguments = new(),
                    PaperJvmArguments = new(),
                    OverridePaperJvmArguments = new(),
                    JavaSettings = new(),
                    SevenZipSettings = new(),
                    ModSettings = new(),
                    OverrideModSettings = new(),
                };

            Json.Save(_settingsFilePath, settings, SettingsContext.Default);
        }

        NotificationProvider.Info("launcher.settings.using", _settingsFilePath);
    }

    /// <summary>
    /// Save a Settings object to the Settings file path.
    /// </summary>
    public static void Save(Settings settings)
    {
        if (!VFS.Exists(_settingsFilePath))
            return;

        Settings? existingSettings = Load();
        if (existingSettings == settings)
            return;

        Json.Save(_settingsFilePath, settings, SettingsContext.Default);
    }

    /// <summary>
    /// Load a Settings object from the Settings file path.
    /// </summary>
    public static Settings? Load()
    {
        if (VFS.Exists(_settingsFilePath))
        {
            Settings? inputJson = Json.Load<Settings>(_settingsFilePath, SettingsContext.Default);
            if (inputJson != null)
                return inputJson;

            NotificationProvider.Error("launcher.settings.missing", SettingsFileName, AssemblyConstants.DataDirectory);
            return null;
        }

        NotificationProvider.Error("launcher.settings.missing", SettingsFileName, AssemblyConstants.DataDirectory);
        return null;
    }
}
