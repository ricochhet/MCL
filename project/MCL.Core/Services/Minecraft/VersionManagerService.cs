using System;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Helpers.Paper;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Services.Minecraft;

public static class VersionManagerService
{
    private static string[] args = [];
    private static Config Config;

    public static async Task<bool> Init(Config config, string value)
    {
        Config = config;
        args = value.Split(";");
        if (args.Length != Enum.GetNames(typeof(VersionArgs)).Length)
            return false;
        if (!await TryParse())
            return false;
        return true;
    }

    private static async Task<bool> TryParse()
    {
        if (!await VersionHelper.SetVersions(Config, args))
            return false;
        if (!await FabricVersionHelper.SetVersions(Config, args))
            return false;
        if (!await QuiltVersionHelper.SetVersions(Config, args))
            return false;
        if (!await PaperVersionHelper.SetVersions(Config, args))
            return false;
        return true;
    }
}
