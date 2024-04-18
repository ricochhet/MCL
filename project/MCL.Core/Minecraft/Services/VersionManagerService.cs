using System;
using System.Threading.Tasks;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.Servers.Paper.Helpers;

namespace MCL.Core.Minecraft.Services;

public static class VersionManagerService
{
    private static string[] args = [];
    private static Settings Settings;

    public static async Task<bool> Init(Settings settings, string value)
    {
        Settings = settings;
        args = value.Split(";");
        if (args.Length != Enum.GetNames(typeof(VersionArgs)).Length)
            return false;
        if (!await TryParse())
            return false;
        return true;
    }

    private static async Task<bool> TryParse()
    {
        if (!await VersionHelper.SetVersions(Settings, args))
            return false;
        if (!await FabricVersionHelper.SetVersions(Settings, args))
            return false;
        if (!await QuiltVersionHelper.SetVersions(Settings, args))
            return false;
        if (!await PaperVersionHelper.SetVersions(Settings, args))
            return false;
        return true;
    }
}
