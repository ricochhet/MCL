using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Providers;

namespace MCL.Core.Config;

public static class ConfigProvider
{
    public const string DataPath = "./MCL_Data";
    public const string ConfigFileName = "Config.json";
    public static readonly string LogFilePath = Path.Combine(DataPath, LogFileName);
    public static readonly List<string> WatermarkText =
    [
        "MCL.Launcher",
        "This work is free of charge",
        "If you paid money, you were scammed"
    ];
    private const string LogFileName = "MCL.log";
    private static readonly string ConfigFilePath = Path.Combine(DataPath, ConfigFileName);

    public static void Write()
    {
        if (!FsProvider.Exists(ConfigFilePath))
        {
            LogBase.Info("Setup: Creating config...");
            Models.Config config =
                new()
                {
                    MinecraftUrls = new(),
                    FabricUrls = new(),
                    MinecraftArgs = new(),
                    FabricArgs = new()
                };

            JsonSerializerOptions options = new() { WriteIndented = true };
            Json.Write(DataPath, ConfigFileName, config, options);
        }
    }

    public static Models.Config Read()
    {
        if (FsProvider.Exists(ConfigFilePath))
        {
            Models.Config inputJson = Json.Read<Models.Config>(ConfigFilePath);
            if (inputJson != null)
            {
                return inputJson;
            }

            return null;
        }

        return null;
    }
}
