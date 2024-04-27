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

using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ServerProperties
{
    /// <summary>
    /// Create a new Eula text file in the specified server path.
    /// </summary>
    public static void NewEula(LauncherPath? launcherPath)
    {
        string filepath = VFS.Combine(MPathResolver.ServerPath(launcherPath), "eula.txt");
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "eula=true\n");
    }

    /// <summary>
    /// Create a new Properties file in the specified server path.
    /// </summary>
    public static void NewProperties(LauncherPath? launcherPath)
    {
        string filepath = VFS.Combine(MPathResolver.ServerPath(launcherPath), "server.properties");
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "online-mode=false\n");
    }
}
