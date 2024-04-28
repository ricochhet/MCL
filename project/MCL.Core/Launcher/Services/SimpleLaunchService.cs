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

#pragma warning disable CS8602
        settings.LauncherSettings.ClientType = EnumResolver.Parse(
            options.GetValueOrDefault("client", "vanilla"),
            ClientType.VANILLA
        );
        settings.LauncherVersion.MVersion = options.GetValueOrDefault("gameversion", settings.LauncherVersion.MVersion);
        settings.LauncherVersion.FabricLoaderVersion = options.GetValueOrDefault(
            "fabricversion",
            settings.LauncherVersion.FabricLoaderVersion
        );
        settings.LauncherVersion.QuiltLoaderVersion = options.GetValueOrDefault(
            "quiltversion",
            settings.LauncherVersion.QuiltLoaderVersion
        );
        settings.LauncherUsername.Username = options.GetValueOrDefault("username", settings.LauncherUsername.Username);
#pragma warning restore CS8602

        NotificationService.Info("launcher.simple.launch");
        MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
    }
}
