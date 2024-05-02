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
using MCL.Core.FileExtractors.Models;
using MCL.Core.FileExtractors.Services;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Cryptography.Helpers;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.Modding.Enums;
using MCL.Core.Modding.Models;
using MCL.Core.Modding.Resolvers;

namespace MCL.Core.Modding.Services;

public class ModdingService
{
    private readonly LauncherPath? LauncherPath;
    private readonly SevenZipSettings? SevenZipSettings;
    public readonly ModSettings? ModSettings;

    private ModdingService() { }

    public ModdingService(LauncherPath? launcherPath, SevenZipSettings? sevenZipSettings, ModSettings? modSettings)
    {
        LauncherPath = launcherPath;
        SevenZipSettings = sevenZipSettings;
        ModSettings = modSettings;
        if (!VFS.Exists(LauncherPath?.ModPath ?? ValidationShims.StringEmpty()))
            VFS.CreateDirectory(launcherPath?.ModPath ?? ValidationShims.StringEmpty());
    }

    /// <summary>
    /// Save mod store data from the mod store location.
    /// </summary>
    public bool Save(string? modStoreName)
    {
        string modPath = ModPathResolver.ModPath(LauncherPath, modStoreName);
        if (!VFS.Exists(modPath))
        {
            NotificationProvider.Error("modding.save.error-nodir", modPath);
            return false;
        }

        string[] modFilePaths = VFS.GetFiles(modPath, "*", SearchOption.TopDirectoryOnly);
        if (modFilePaths.Length <= 0)
        {
            NotificationProvider.Error("modding.save.error-nofile", modPath);
            return false;
        }

        string[] fileTypes = [.. ModSettings?.CopyOnlyTypes, .. ModSettings?.UnzipAndCopyTypes];
        string[] filteredModFilePaths = modFilePaths
            .Where(file => Array.Exists(fileTypes, file.ToLower().EndsWith))
            .ToArray();
        if (filteredModFilePaths.Length <= 0)
        {
            NotificationProvider.Error("modding.save.error-nofile", modPath);
            return false;
        }

        ModFiles modFiles = new();
        foreach (string modFilePath in filteredModFilePaths)
        {
            if (ModSettings?.CopyOnlyTypes.Contains(VFS.GetFileExtension(modFilePath)) ?? false)
                modFiles.Files.Add(
                    new(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.COPY_ONLY)
                );
            else if (ModSettings?.UnzipAndCopyTypes.Contains(VFS.GetFileExtension(modFilePath)) ?? false)
                modFiles.Files.Add(
                    new(modFilePath, CryptographyHelper.CreateSHA1(modFilePath, true), ModRule.UNZIP_AND_COPY)
                );
        }
        string filepath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!ModSettings?.IsStoreSaved(modStoreName) ?? false)
#pragma warning disable IDE0079
#pragma warning disable S2589
            ModSettings?.ModStores.Add(modStoreName ?? ValidationShims.StringEmpty());
#pragma warning restore IDE0079, S2589
        Json.Save(
            filepath,
            modFiles,
            new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }
        );

        return true;
    }

    /// <summary>
    /// Register a mod store path to ModSettings.
    /// </summary>
    public void Register(string modStoreName)
    {
        if (
            VFS.Exists(ModPathResolver.ModStorePath(LauncherPath, modStoreName))
            && (!ModSettings?.IsStoreSaved(modStoreName) ?? false)
        )
#pragma warning disable IDE0079
#pragma warning disable S2589
            ModSettings?.ModStores.Add(modStoreName);
#pragma warning restore IDE0079, S2589
    }

    /// <summary>
    /// Load ModFiles from the mod store data file.
    /// </summary>
    public ModFiles? Load(string? modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (VFS.Exists(modStorePath) && (ModSettings?.IsStoreSaved(modStoreName) ?? false))
            return Json.Load<ModFiles>(modStorePath);
        return default;
    }

    /// <summary>
    /// Delete a mod store data file.
    /// </summary>
    public bool Delete(string modStoreName)
    {
        string modStorePath = ModPathResolver.ModStorePath(LauncherPath, modStoreName);
        if (!VFS.Exists(modStorePath))
            return false;

        if (ModSettings?.IsStoreSaved(modStoreName) ?? false)
#pragma warning disable IDE0079
#pragma warning disable S2589
            ModSettings?.ModStores.Remove(modStoreName);
#pragma warning restore IDE0079, S2589

        VFS.DeleteFile(modStorePath);
        return true;
    }

    /// <summary>
    /// Delete a mod store deployment path from the data file.
    /// </summary>
    public bool DeleteSavedDeployPath(string deployPath)
    {
        if (ModSettings?.IsDeployPathSaved(deployPath) ?? false)
#pragma warning disable IDE0079
#pragma warning disable S2589
            ModSettings?.DeployPaths.Remove(deployPath);
#pragma warning restore IDE0079, S2589
        else
            return false;

        return true;
    }

    /// <summary>
    /// Deploy ModFiles to the specified deployment path.
    /// </summary>
    public bool Deploy(ModFiles? modFiles, string deployPath, bool overwrite = false)
    {
        if (ObjectValidator<List<ModFile>>.IsNullOrEmpty(modFiles?.Files))
        {
            NotificationProvider.Error("modding.deploy.error-nofile");
            return false;
        }

        if (!VFS.Exists(deployPath))
            VFS.CreateDirectory(deployPath);
        else
        {
            VFS.DeleteDirectory(deployPath);
            VFS.CreateDirectory(deployPath);
        }

        List<ModFile> sortedModFiles = [.. modFiles!.Files!.OrderBy(a => a.Priority)];
        ModSettings?.DeployPaths.Add(deployPath);

        foreach (ModFile modFile in sortedModFiles)
        {
            if (ObjectValidator<string>.IsNullOrWhiteSpace([modFile?.ModPath]))
            {
                NotificationProvider.Error("modding.deploy.error-nodata");
                return false;
            }

            if (!VFS.Exists(modFile!.ModPath!))
                continue;

            if (VFS.Exists(VFS.Combine(deployPath, VFS.GetFileName(modFile!.ModPath!))) && !overwrite)
                continue;

            switch (modFile!.ModRule!)
            {
                case ModRule.COPY_ONLY:
                    VFS.CopyFile(modFile!.ModPath!, VFS.Combine(deployPath, VFS.GetFileName(modFile!.ModPath!)));
                    break;
                case ModRule.UNZIP_AND_COPY:
                    SevenZipService sz = new(SevenZipSettings);
                    sz.Extract(modFile!.ModPath!, deployPath);
                    break;
            }
        }

        return true;
    }
}
