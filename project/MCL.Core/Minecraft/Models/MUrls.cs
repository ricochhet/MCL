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

namespace MCL.Core.Minecraft.Models;

public class MUrls
{
    public string VersionManifest { get; set; } = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
    public string PistonVersionManifest { get; set; } = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
    public string MinecraftResources { get; set; } = "https://resources.download.minecraft.net";
    public string JavaVersionManifest { get; set; } =
        "https://launchermeta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json";

    public MUrls() { }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MUrls))]
internal partial class MUrlsContext : JsonSerializerContext { }
