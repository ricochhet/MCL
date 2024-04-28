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

using System;
using System.Collections.Generic;
using System.Text.Json;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.Launcher.Services;

public static class SettingsService
{
    public const string DataDirectory = ".mcl";
    public const string SettingsFileName = "mcl.json";
    public const string SimpleLaunchFileName = "launch.txt";
    public const string LocalizationDirectory = "localization";
    public const string LogsDirectory = "logs";
    public static readonly string LogFilePath = VFS.FromCwd(
        DataDirectory,
        LogsDirectory,
        $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log"
    );
    public static readonly string LocalizationPath = VFS.FromCwd(DataDirectory, LocalizationDirectory);
    public static readonly string SimpleLaunchFilePath = VFS.FromCwd(DataDirectory, SimpleLaunchFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private static readonly string _settingsFilePath = VFS.FromCwd(DataDirectory, SettingsFileName);

    public static JsonSerializerOptions JsonSerializerOptions { get; set; } = Json.JsonSerializerOptions;

    /// <summary>
    /// Initialize the Settings service.
    /// Create a new configuration file if one is not present.
    /// </summary>
    public static void Init()
    {
        if (!VFS.Exists(_settingsFilePath))
        {
            NotificationService.Warn("launcher.settings.missing", SettingsFileName, DataDirectory);
            NotificationService.Info("launcher.settings.setup", _settingsFilePath);
            Settings settings =
                new()
                {
                    MainClassNames = new(),
                    LauncherMemory = new(),
                    LauncherInstance = new(),
                    OverrideLauncherInstance = new(),
                    LauncherUsername = new(username: "Player1337"),
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

            Json.Save(_settingsFilePath, settings, JsonSerializerOptions);
        }

        NotificationService.Info("launcher.settings.using", _settingsFilePath);
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

        Json.Save(_settingsFilePath, settings, JsonSerializerOptions);
    }

    /// <summary>
    /// Load a Settings object from the Settings file path.
    /// </summary>
    public static Settings? Load()
    {
        if (VFS.Exists(_settingsFilePath))
        {
            Settings? inputJson = Json.Load<Settings>(_settingsFilePath);
            if (inputJson != null)
                return inputJson;

            NotificationService.Error("launcher.settings.missing", SettingsFileName, DataDirectory);
            return null;
        }

        NotificationService.Error("launcher.settings.missing", SettingsFileName, DataDirectory);
        return null;
    }
}
