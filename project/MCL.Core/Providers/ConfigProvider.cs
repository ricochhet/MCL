using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;

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

    public static void Write()
    {
        if (!VFS.Exists(ConfigFilePath))
        {
            LogBase.Info("Setup: Creating config...");
            Config config =
                new()
                {
                    MinecraftUrls = new(),
                    FabricUrls = new(),
                    MinecraftArgs = new(),
                    FabricArgs = new()
                };

            JsonSerializerOptions options = new() { WriteIndented = true };
            Json.Save(ConfigFilePath, config, options);
        }
    }

    public static void Write(Config config)
    {
        if (!VFS.Exists(ConfigFilePath))
            return;

        Config existingConfig = Read();
        if (existingConfig == config)
            return;

        JsonSerializerOptions options =
            new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        Json.Save(ConfigFilePath, config, options);
    }

    public static Config Read()
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
