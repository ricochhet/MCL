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

using MCL.Core.Modding.Enums;

namespace MCL.Core.Modding.Models;

public class ModFile
{
    public string ModPath { get; set; }
    public string SHA1 { get; set; }
    public ModRule ModRule { get; set; }
    public int Priority { get; set; }

    public ModFile() { }

    public ModFile(string modPath, string sha1, ModRule modRule, int priority = 0)
    {
        ModPath = modPath;
        SHA1 = sha1;
        ModRule = modRule;
        Priority = priority;
    }
}
