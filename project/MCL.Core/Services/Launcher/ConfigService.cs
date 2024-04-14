using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Services.Launcher;

public static class ConfigService
{
    public const string DataPath = "./.mcl";
    public const string ConfigFileName = "mcl.json";
    public static readonly string LogFilePath = VFS.FromCwd(DataPath, LogFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private const string LogFileName = "mcl.log";
    private static readonly string ConfigFilePath = VFS.FromCwd(DataPath, ConfigFileName);

    public static JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    public static void Save()
    {
        if (!VFS.Exists(ConfigFilePath))
        {
            NotificationService.Add(new(NativeLogLevel.Info, "launcher.config.setup"));
            Config config =
                new()
                {
                    MinecraftUrls = new(),
                    FabricUrls = new(),
                    QuiltUrls = new(),
                    MinecraftArgs = new(),
                    FabricArgs = new(),
                    QuiltArgs = new(),
                    JavaConfig = new(),
                    SevenZipConfig = new(),
                    ModConfig = new()
                };

            Json.Save(ConfigFilePath, config, JsonSerializerOptions);
        }
    }

    public static void Save(Config config)
    {
        if (!VFS.Exists(ConfigFilePath))
            return;

        Config existingConfig = Load();
        if (existingConfig == config)
            return;

        Json.Save(ConfigFilePath, config, JsonSerializerOptions);
    }

    public static Config Load()
    {
        if (VFS.Exists(ConfigFilePath))
        {
            Config inputJson = Json.Load<Config>(ConfigFilePath);
            if (inputJson != null)
                return inputJson;

            NotificationService.Add(new(NativeLogLevel.Error, "launcher.config.missing", [ConfigFileName, DataPath]));
            return null;
        }

        NotificationService.Add(new(NativeLogLevel.Error, "launcher.config.missing", [ConfigFileName, DataPath]));
        return null;
    }
}
