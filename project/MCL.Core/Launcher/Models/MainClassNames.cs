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

using System.Text.Json.Serialization;

namespace MCL.Core.Launcher.Models;

public class MainClassNames
{
    public string Vanilla { get; set; } = "net.minecraft.client.main.Main";
    public string Fabric { get; set; } = "net.fabricmc.loader.impl.launch.knot.KnotClient";
    public string Quilt { get; set; } = "org.quiltmc.loader.impl.launch.knot.KnotClient";
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MainClassNames))]
internal partial class MainClassNamesContext : JsonSerializerContext;
