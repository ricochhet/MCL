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

public class JavaVersionManifest
{
    [JsonPropertyName("gamecore")]
    public JavaRuntime? Gamecore { get; set; }

    [JsonPropertyName("linux")]
    public JavaRuntime? Linux { get; set; }

    [JsonPropertyName("linux-i386")]
    public JavaRuntime? LinuxI386 { get; set; }

    [JsonPropertyName("mac-os")]
    public JavaRuntime? Macos { get; set; }

    [JsonPropertyName("mac-os-arm64")]
    public JavaRuntime? MacosArm64 { get; set; }

    [JsonPropertyName("windows-arm64")]
    public JavaRuntime? WindowsArm64 { get; set; }

    [JsonPropertyName("windows-x64")]
    public JavaRuntime? WindowsX64 { get; set; }

    [JsonPropertyName("windows-x86")]
    public JavaRuntime? WindowsX86 { get; set; }

    public JavaVersionManifest() { }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(JavaVersionManifest))]
internal partial class JavaVersionManifestContext : JsonSerializerContext;
