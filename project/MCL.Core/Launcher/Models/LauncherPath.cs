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
using MCL.Core.Launcher.Providers;
using MCL.Core.MiniCommon.IO;

namespace MCL.Core.Launcher.Models;

public class LauncherPath
{
    public string MPath { get; set; } = VFS.FromCwd(LaunchPathHelper.MPath);
    public string ModPath { get; set; } = VFS.FromCwd(SettingsProvider.DataDirectory, LaunchPathHelper.ModPath);
    public string FabricPath { get; set; } = VFS.FromCwd(SettingsProvider.DataDirectory, LaunchPathHelper.FabricPath);
    public string QuiltPath { get; set; } = VFS.FromCwd(SettingsProvider.DataDirectory, LaunchPathHelper.QuiltPath);
    public string PaperPath { get; set; } = VFS.FromCwd(SettingsProvider.DataDirectory, LaunchPathHelper.PaperPath);

    public LauncherPath() { }

    public LauncherPath(string path, string modPath, string fabricPath, string quiltPath, string paperPath)
    {
        MPath = VFS.FromCwd(path);
        ModPath = VFS.FromCwd(SettingsProvider.DataDirectory, modPath);
        FabricPath = VFS.FromCwd(SettingsProvider.DataDirectory, fabricPath);
        QuiltPath = VFS.FromCwd(SettingsProvider.DataDirectory, quiltPath);
        PaperPath = VFS.FromCwd(SettingsProvider.DataDirectory, paperPath);
    }
}
