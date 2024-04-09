using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using MCL.Core.Enums.Services;
using MCL.Core.Helpers;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
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
        if (!VFS.Exists(LauncherPath.ModPath))
            VFS.CreateDirectory(launcherPath.ModPath);
    }

    public static void Save(string modStoreName)
    {
        if (!VFS.Exists(ModPathResolver.ModPath(LauncherPath, modStoreName)))
            return;

        string[] modFilePaths = VFS.GetFiles(
            ModPathResolver.ModPath(LauncherPath, modStoreName),
            "*",
            SearchOption.TopDirectoryOnly
        );
        if (modFilePaths.Length <= 0)
            return;

        string[] filteredModFilePaths = modFilePaths
            .Where(file => ModConfig.FileTypes.Any(file.ToLower().EndsWith))
            .ToArray();
        if (filteredModFilePaths.Length <= 0)
            return;

        ModFiles modFiles = new();
        foreach (string modFilePath in filteredModFilePaths)
        {
            if (ModConfig.CopyOnlyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.Sha1(modFilePath, true), ModRuleEnum.COPY_ONLY)
                );
            else if (ModConfig.UnzipAndCopyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.Sha1(modFilePath, true), ModRuleEnum.UNZIP_AND_COPY)
                );
        }
        string filepath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!ModConfig.IsStoreRegistered(modStoreName))
            ModConfig.RegisteredStores.Add(modStoreName);
        Json.Save(filepath, modFiles, new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping  });
    }

    public static ModFiles Load(string modStoreName)
    {
        if (
            VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName))
            && ModConfig.IsStoreRegistered(modStoreName)
        )
            return Json.Load<ModFiles>(ModPathResolver.ModStorePath(LauncherPath, modStoreName));
        return default;
    }

    public static void Delete(string modStoreName)
    {
        if (!VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName)))
            return;

        ModConfig.RegisteredStores.Remove(modStoreName);
        VFS.DeleteFile(ModPathResolver.ModStorePath(LauncherPath, modStoreName));
    }

    public static void Deploy(ModFiles modFiles, string deployPath)
    {
        if (modFiles?.Files?.Count <= 0)
            return;

        List<ModFile> sortedModFiles = [.. modFiles.Files.OrderBy(a => a.Priority)];
        foreach (ModFile modFile in sortedModFiles)
        {
            switch (modFile.ModRule)
            {
                case ModRuleEnum.COPY_ONLY:
                    VFS.CopyFile(modFile.ModPath, VFS.Combine(deployPath, VFS.GetFileName(modFile.ModPath)));
                    break;
                case ModRuleEnum.UNZIP_AND_COPY:
                    throw new NotImplementedException();
            }
        }
    }
}
