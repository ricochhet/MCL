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
    private static string[] _args = [];
    private static Settings _settings;

    public static async Task<bool> Init(Settings settings, string value)
    {
        _settings = settings;
        _args = value.Split(";");
        if (_args.Length != Enum.GetNames(typeof(VersionArgs)).Length)
            return false;
        if (!await TryParse())
            return false;
        return true;
    }

    private static async Task<bool> TryParse()
    {
        if (!await VersionHelper.SetVersions(_settings, _args))
            return false;
        if (!await FabricVersionHelper.SetVersions(_settings, _args))
            return false;
        if (!await QuiltVersionHelper.SetVersions(_settings, _args))
            return false;
        if (!await PaperVersionHelper.SetVersions(_settings, _args))
            return false;
        return true;
    }
}
