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

namespace MCL.Core.Java.Models;

public class JavaRuntimeObject(
    JavaRuntimeAvailability javaRuntimeAvailability,
    JavaRuntimeManifest javaRuntimeManifest,
    JavaRuntimeVersion javaRuntimeVersion
)
{
    [JsonPropertyName("availability")]
    public JavaRuntimeAvailability JavaRuntimeAvailability { get; set; } = javaRuntimeAvailability;

    [JsonPropertyName("manifest")]
    public JavaRuntimeManifest JavaRuntimeManifest { get; set; } = javaRuntimeManifest;

    [JsonPropertyName("version")]
    public JavaRuntimeVersion JavaRuntimeVersion { get; set; } = javaRuntimeVersion;
}
