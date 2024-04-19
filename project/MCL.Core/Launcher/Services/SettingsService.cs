using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Launcher.Models;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Services;

public static class SettingsService
{
    public const string DataPath = "./.mcl";
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
                    LauncherInstance = new(),
                    LauncherUsername = new(username: "Player1337"),
                    LauncherPath = new(),
                    LauncherVersion = new(),
                    LauncherSettings = new(),
                    MUrls = new(),
                    FabricUrls = new(),
                    QuiltUrls = new(),
                    PaperUrls = new(),
                    MJvmArguments = new(),
                    FabricJvmArguments = new(),
                    QuiltJvmArguments = new(),
                    JavaSettings = new(),
                    SevenZipSettings = new(),
                    ModSettings = new()
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

            NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", [SettingsFileName, DataPath]);
            return null;
        }

        NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", [SettingsFileName, DataPath]);
        return null;
    }
}
