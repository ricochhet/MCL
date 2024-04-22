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

using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Models;

public class LauncherPath
{
    public string Path { get; set; } = VFS.FromCwd(LaunchPathHelper.Path);
    public string ModPath { get; set; } = VFS.FromCwd(SettingsService.DataDirectory, LaunchPathHelper.ModPath);
    public string FabricPath { get; set; } = VFS.FromCwd(SettingsService.DataDirectory, LaunchPathHelper.FabricPath);
    public string QuiltPath { get; set; } = VFS.FromCwd(SettingsService.DataDirectory, LaunchPathHelper.QuiltPath);
    public string PaperPath { get; set; } = VFS.FromCwd(SettingsService.DataDirectory, LaunchPathHelper.PaperPath);

    public LauncherPath() { }

    public LauncherPath(string path, string modPath, string fabricPath, string quiltPath, string paperPath)
    {
        Path = VFS.FromCwd(path);
        ModPath = VFS.FromCwd(SettingsService.DataDirectory, modPath);
        FabricPath = VFS.FromCwd(SettingsService.DataDirectory, fabricPath);
        QuiltPath = VFS.FromCwd(SettingsService.DataDirectory, quiltPath);
        PaperPath = VFS.FromCwd(SettingsService.DataDirectory, paperPath);
    }
}
