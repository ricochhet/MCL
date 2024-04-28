using System.Collections.Generic;
using System.Linq;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon.Commands;
using MCL.Core.MiniCommon.FileSystem;
using MCL.Core.MiniCommon.Resolvers;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Launcher.Services;

public static class SimpleLaunchService
{
    public static void Init(string filePath, Settings? settings)
    {
        if (!VFS.Exists(filePath))
            return;

        if (
            ObjectValidator<LauncherSettings>.IsNull(settings?.LauncherSettings)
            || ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion)
            || ObjectValidator<LauncherUsername>.IsNull(settings?.LauncherUsername)
        )
            return;

        string[] lines = VFS.ReadAllLines(filePath);
        Dictionary<string, string> options = [];
        foreach (string line in lines)
            options = options.Concat(CommandLine.ParseKeyValuePairs(line)).ToDictionary();
        options = options.GroupBy(a => a).Select(a => a.Last()).ToDictionary();
        string javaPath = string.Empty;
        foreach ((string key, string value) in options)
        {
            switch (key)
            {
                case "client":
#pragma warning disable CS8602
                    settings.LauncherSettings.ClientType = EnumResolver.Parse(value, ClientType.VANILLA);
#pragma warning restore CS8602
                    break;
                case "gameversion":
#pragma warning disable CS8602
                    settings.LauncherVersion.MVersion = value;
#pragma warning restore CS8602
                    break;
                case "fabricversion":
#pragma warning disable CS8602
                    settings.LauncherVersion.FabricLoaderVersion = value;
#pragma warning restore CS8602
                    break;
                case "quiltversion":
#pragma warning disable CS8602
                    settings.LauncherVersion.QuiltLoaderVersion = value;
#pragma warning restore CS8602
                    break;
                case "username":
#pragma warning disable CS8602
                    settings.LauncherUsername.Username = value;
#pragma warning restore CS8602
                    break;
                case "javapath":
                    javaPath = value;
                    break;
                default:
                    break;
            }
        }

        NotificationService.Info("launcher.simple.launch");
        MinecraftLauncher.Launch(settings ?? ValidationShims.ClassEmpty<Settings>(), javaPath);
    }
}
