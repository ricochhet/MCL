/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using MCL.Core.FileExtractors.Services;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Helpers;
using MCL.Core.MiniCommon.Services;
using MCL.Core.Modding.Enums;
using MCL.Core.Modding.Models;
using MCL.Core.Modding.Resolvers;

namespace MCL.Core.Modding.Services;

public static class ModdingService
{
    public static LauncherPath LauncherPath { get; private set; }
    public static ModSettings ModSettings { get; private set; }

    static ModdingService() { }

    public static void Init(LauncherPath launcherPath, ModSettings modSettings)
    {
        LauncherPath = launcherPath;
        ModSettings = modSettings;
        if (!VFS.Exists(LauncherPath.ModPath))
            VFS.CreateDirectory(launcherPath.ModPath);
    }

    public static bool Save(string modStoreName)
    {
        string modPath = ModPathResolver.ModPath(LauncherPath, modStoreName);
        if (!VFS.Exists(modPath))
        {
            NotificationService.Error("modding.save.error-nodir", modPath);
            return false;
        }

        string[] modFilePaths = VFS.GetFiles(modPath, "*", SearchOption.TopDirectoryOnly);
        if (modFilePaths.Length <= 0)
        {
            NotificationService.Error("modding.save.error-nofile", modPath);
            return false;
        }

        string[] fileTypes = [.. ModSettings.CopyOnlyTypes, .. ModSettings.UnzipAndCopyTypes];
        string[] filteredModFilePaths = modFilePaths
            .Where(file => Array.Exists(fileTypes, file.ToLower().EndsWith))
            .ToArray();
        if (filteredModFilePaths.Length <= 0)
        {
            NotificationService.Error("modding.save.error-nofile", modPath);
            return false;
        }

        ModFiles modFiles = new();
        foreach (string modFilePath in filteredModFilePaths)
        {
            if (ModSettings.CopyOnlyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.COPY_ONLY)
                );
            else if (ModSettings.UnzipAndCopyTypes.Contains(VFS.GetFileExtension(modFilePath)))
                modFiles.Files.Add(
                    new ModFile(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.UNZIP_AND_COPY)
                );
        }
        string filepath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!ModSettings.IsStoreSaved(modStoreName))
            ModSettings.ModStores.Add(modStoreName);
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
            && !ModSettings.IsStoreSaved(modStoreName)
        )
            ModSettings.ModStores.Add(modStoreName);
    }

    public static ModFiles Load(string modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (VFS.Exists(modStorePath) && ModSettings.IsStoreSaved(modStoreName))
            return Json.Load<ModFiles>(modStorePath);
        return default;
    }

    public static bool Delete(string modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!VFS.Exists(modStorePath))
            return false;

        if (ModSettings.IsStoreSaved(modStoreName))
            ModSettings.ModStores.Remove(modStoreName);

        VFS.DeleteFile(modStorePath);
        return true;
    }

    public static bool DeleteSavedDeployPath(string deployPath)
    {
        if (ModSettings.IsDeployPathSaved(deployPath))
            ModSettings.DeployPaths.Remove(deployPath);
        else
            return false;

        return true;
    }

    public static bool Deploy(ModFiles modFiles, string deployPath, bool overwrite = false)
    {
        if (ObjectValidator<List<ModFile>>.IsNullOrEmpty(modFiles?.Files))
        {
            NotificationService.Error("modding.deploy.error-nofile");
            return false;
        }

        if (!VFS.Exists(deployPath))
            VFS.CreateDirectory(deployPath);
        else
        {
            VFS.DeleteDirectory(deployPath);
            VFS.CreateDirectory(deployPath);
        }

        List<ModFile> sortedModFiles = [.. modFiles.Files.OrderBy(a => a.Priority)];
        ModSettings.DeployPaths.Add(deployPath);

        foreach (ModFile modFile in sortedModFiles)
        {
            if (ObjectValidator<ModFile>.IsNull(modFile))
            {
                NotificationService.Error("modding.deploy.error-nodata");
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
                    SevenZipService.Extract(modFile.ModPath, deployPath);
                    break;
            }
        }

        return true;
    }
}
