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
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Launcher.Models;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Services;

public static class SettingsService
{
    public const string DataPath = ".mcl";
    public const string SettingsFileName = "mcl.json";
    public static readonly string LogFilePath = VFS.FromCwd(DataPath, _logFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private const string _logFileName = "mcl.log";
    private static readonly string _settingsFilePath = VFS.FromCwd(DataPath, SettingsFileName);

    public static JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    public static void Save()
    {
        if (!VFS.Exists(_settingsFilePath))
        {
            NotificationService.Log(NativeLogLevel.Info, "launcher.settings.setup");
            Settings settings =
                new()
                {
                    MainClassNames = new(),
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
    }

    public static void Save(Settings settings)
    {
        if (!VFS.Exists(_settingsFilePath))
            return;

        Settings existingSettings = Load();
        if (existingSettings == settings)
            return;

        Json.Save(_settingsFilePath, settings, JsonSerializerOptions);
    }

    public static Settings Load()
    {
        if (VFS.Exists(_settingsFilePath))
        {
            Settings inputJson = Json.Load<Settings>(_settingsFilePath);
            if (inputJson != null)
                return inputJson;

            NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", SettingsFileName, DataPath);
            return null;
        }

        NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", SettingsFileName, DataPath);
        return null;
    }
}
