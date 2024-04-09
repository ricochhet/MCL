using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Modding;
using MCL.Core.Resolvers.Services;
using MCL.Core.Services.Modding;

namespace MCL.Core.Services;

public static class ModdingService
{
    public static MCLauncherPath LauncherPath { get; private set; }
    public static ModConfig ModConfig { get; private set; }

    static ModdingService() { }

    public static void Init(MCLauncherPath launcherPath, ModConfig modConfig)
    {
        LauncherPath = launcherPath;
        ModConfig = modConfig;
    }

    public static void Save(string modStoreName)
    {
        if (!VFS.Exists(ModPathResolver.ModPath(LauncherPath, modStoreName)))
            return;

        string[] mods = VFS.GetFiles(
            ModPathResolver.ModPath(LauncherPath, modStoreName),
            "*",
            SearchOption.TopDirectoryOnly
        );
        if (mods.Length <= 0)
            return;

        string[] filteredMods = mods.Where(file => ModConfig.FileTypes.Any(file.ToLower().EndsWith)).ToArray();
        if (filteredMods.Length <= 0)
            return;

        ModFiles modFiles = new();
        foreach (string mod in filteredMods)
        {
            modFiles.Files.Add(new ModFile(mod, CryptographyHelper.Sha1(mod, true)));
        }
        string filepath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!VFS.Exists(filepath))
            Json.Save(filepath, modFiles);
    }

    public static ModFiles Load(string modStoreName)
    {
        if (VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName)))
            return Json.Load<ModFiles>(ModPathResolver.ModStorePath(LauncherPath, modStoreName));
        return default;
    }

    public static void Delete(string modStoreName)
    {
        if (VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName)))
            VFS.DeleteFile(ModPathResolver.ModStorePath(LauncherPath, modStoreName));
    }
}
