using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using MCL.Core.Enums.Services;
using MCL.Core.Helpers;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Modding;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Services.Modding;

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

    public static bool Save(string modStoreName)
    {
        string modPath = ModPathResolver.ModPath(LauncherPath, modStoreName);
        if (!VFS.Exists(modPath))
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.save.error-nodir", [modPath]));
            return false;
        }

        string[] modFilePaths = VFS.GetFiles(modPath, "*", SearchOption.TopDirectoryOnly);
        if (modFilePaths.Length <= 0)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.save.error-nofile", [modPath]));
            return false;
        }

        string[] fileTypes = [.. ModConfig.CopyOnlyTypes, .. ModConfig.UnzipAndCopyTypes];
        string[] filteredModFilePaths = modFilePaths
            .Where(file => Array.Exists(fileTypes, file.ToLower().EndsWith))
            .ToArray();
        if (filteredModFilePaths.Length <= 0)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.save.error-nofile", [modPath]));
            return false;
        }

        ModFiles modFiles = new();
        foreach (string modFilePath in filteredModFilePaths)
        {
            if (ModConfig.CopyOnlyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.COPY_ONLY)
                );
            else if (ModConfig.UnzipAndCopyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.UNZIP_AND_COPY)
                );
        }
        string filepath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!ModConfig.IsStoreRegistered(modStoreName))
            ModConfig.RegisteredStores.Add(modStoreName);
        Json.Save(
            filepath,
            modFiles,
            new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }
        );

        return true;
    }

    public static void Register(string modStoreName)
    {
        if (
            VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName))
            && !ModConfig.IsStoreRegistered(modStoreName)
        )
            ModConfig.RegisteredStores.Add(modStoreName);
    }

    public static ModFiles Load(string modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (VFS.Exists(modStorePath) && ModConfig.IsStoreRegistered(modStoreName))
            return Json.Load<ModFiles>(modStorePath);
        return default;
    }

    public static bool Delete(string modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!VFS.Exists(modStorePath))
            return false;

        if (ModConfig.IsStoreRegistered(modStoreName))
            ModConfig.RegisteredStores.Remove(modStoreName);

        VFS.DeleteFile(modStorePath);
        return true;
    }

    public static bool Deploy(ModFiles modFiles, string deployPath, bool overwrite = false)
    {
        if (modFiles == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.deploy.error-nofile"));
            return false;
        }

        if (modFiles.Files?.Count <= 0)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.deploy.error-nofile"));
            return false;
        }

        if (!VFS.Exists(deployPath))
            VFS.CreateDirectory(deployPath);

        List<ModFile> sortedModFiles = [.. modFiles.Files.OrderBy(a => a.Priority)];

        foreach (ModFile modFile in sortedModFiles)
        {
            if (modFile == null)
            {
                NotificationService.Add(new Notification(NativeLogLevel.Error, "modding.deploy.error-nodata"));
                return false;
            }

            if (!VFS.Exists(modFile.ModPath))
                continue;

            if (VFS.Exists(VFS.Combine(deployPath, VFS.GetFileName(modFile.ModPath))) && !overwrite)
                continue;

            switch (modFile.ModRule)
            {
                case ModRule.COPY_ONLY:
                    VFS.CopyFile(modFile.ModPath, VFS.Combine(deployPath, VFS.GetFileName(modFile.ModPath)));
                    break;
                case ModRule.UNZIP_AND_COPY:
                    throw new NotImplementedException();
            }
        }

        return true;
    }
}
