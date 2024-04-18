using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Services.Launcher;

public static class SettingsService
{
    public const string DataPath = "./.mcl";
    public const string SettingsFileName = "mcl.json";
    public static readonly string LogFilePath = VFS.FromCwd(DataPath, LogFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private const string LogFileName = "mcl.log";
    private static readonly string SettingsFilePath = VFS.FromCwd(DataPath, SettingsFileName);

    public static JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    public static void Save()
    {
        if (!VFS.Exists(SettingsFilePath))
        {
            NotificationService.Log(NativeLogLevel.Info, "launcher.settings.setup");
            Settings settings =
                new()
                {
                    LauncherUsername = new(username: "Player1337"),
                    LauncherPath = new(),
                    LauncherVersion = new(),
                    LauncherSettings = new(),
                    MinecraftUrls = new(),
                    FabricUrls = new(),
                    QuiltUrls = new(),
                    PaperUrls = new(),
                    MinecraftArgs = new(),
                    FabricArgs = new(),
                    QuiltArgs = new(),
                    JavaSettings = new(),
                    SevenZipSettings = new(),
                    ModSettings = new()
                };

            Json.Save(SettingsFilePath, settings, JsonSerializerOptions);
        }
    }

    public static void Save(Settings settings)
    {
        if (!VFS.Exists(SettingsFilePath))
            return;

        Settings existingSettings = Load();
        if (existingSettings == settings)
            return;

        Json.Save(SettingsFilePath, settings, JsonSerializerOptions);
    }

    public static Settings Load()
    {
        if (VFS.Exists(SettingsFilePath))
        {
            Settings inputJson = Json.Load<Settings>(SettingsFilePath);
            if (inputJson != null)
                return inputJson;

            NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", [SettingsFileName, DataPath]);
            return null;
        }

        NotificationService.Log(NativeLogLevel.Error, "launcher.settings.missing", [SettingsFileName, DataPath]);
        return null;
    }
}
