using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Services;
using MCL.Core.Services;

namespace MCL.Core.Providers;

public static class ConfigProvider
{
    public const string DataPath = "./MCL_Data";
    public const string ConfigFileName = "Config.json";
    public static readonly string LogFilePath = VFS.Combine(DataPath, LogFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private const string LogFileName = "MCL.log";
    private static readonly string ConfigFilePath = VFS.Combine(DataPath, ConfigFileName);

    public static void Save()
    {
        if (!VFS.Exists(ConfigFilePath))
        {
            NotificationService.Add(new Notification(NativeLogLevel.Info, "launcher.config.setup"));
            Config config =
                new()
                {
                    MinecraftUrls = new(),
                    FabricUrls = new(),
                    MinecraftArgs = new(),
                    FabricArgs = new(),
                    JavaConfig = new(),
                    ModConfig = new()
                };

            JsonSerializerOptions options = new() { WriteIndented = true };
            Json.Save(ConfigFilePath, config, options);
        }
    }

    public static void Save(Config config)
    {
        if (!VFS.Exists(ConfigFilePath))
            return;

        Config existingConfig = Load();
        if (existingConfig == config)
            return;

        JsonSerializerOptions options =
            new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        Json.Save(ConfigFilePath, config, options);
    }

    public static Config Load()
    {
        if (VFS.Exists(ConfigFilePath))
        {
            Config inputJson = Json.Load<Config>(ConfigFilePath);
            if (inputJson != null)
                return inputJson;
            return null;
        }

        return null;
    }
}
