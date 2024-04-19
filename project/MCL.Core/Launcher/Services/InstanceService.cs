using System.Text.Encodings.Web;
using System.Text.Json;
using MCL.Core.Launcher.Models;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Services;

public static class InstanceService
{
    public const string InstanceFileName = "instance.json";

    public static JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    private static LauncherPath _launcherPath;
    private static string _instanceFilePath;
    private static bool _loaded = false;

    public static void Init(LauncherPath launcherPath)
    {
        _launcherPath = launcherPath;
        _instanceFilePath = VFS.Combine(launcherPath.Path, InstanceFileName);
        _loaded = true;
    }

    public static void Save()
    {
        if (!_loaded)
            return;

        if (!VFS.Exists(_instanceFilePath))
        {
            NotificationService.Log(NativeLogLevel.Info, "launcher.instance.setup");
            Instance instance =
                new()
                {
                    Versions = [],
                    FabricLoaders = [],
                    QuiltLoaders = [],
                };

            Json.Save(_instanceFilePath, instance, JsonSerializerOptions);
        }
    }

    public static void Save(Instance instance)
    {
        if (!_loaded)
            return;

        if (!VFS.Exists(_instanceFilePath))
            return;

        Instance existingInstance = Load();
        if (existingInstance == instance)
            return;

        Json.Save(_instanceFilePath, instance, JsonSerializerOptions);
    }

    public static Instance Load()
    {
        if (!_loaded)
            return null;

        if (VFS.Exists(_instanceFilePath))
        {
            Instance inputJson = Json.Load<Instance>(_instanceFilePath);
            if (inputJson != null)
                return inputJson;

            NotificationService.Log(
                NativeLogLevel.Error,
                "launcher.settings.missing",
                [_instanceFilePath, _launcherPath.Path]
            );
            return null;
        }

        NotificationService.Log(
            NativeLogLevel.Error,
            "launcher.settings.missing",
            [_instanceFilePath, _launcherPath.Path]
        );
        return null;
    }
}
