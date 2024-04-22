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
using MCL.Core.Java.Enums;

namespace MCL.Core.Java.Resolvers;

public static class JavaRuntimePlatformResolver
{
    public static string ToString(JavaRuntimePlatform type) =>
        type switch
        {
            JavaRuntimePlatform.GAMECORE => "gamecore",
            JavaRuntimePlatform.LINUX => "linux",
            JavaRuntimePlatform.LINUXI386 => "linux-i386",
            JavaRuntimePlatform.MACOS => "mac-os",
            JavaRuntimePlatform.MACOSARM64 => "mac-os-arm64",
            JavaRuntimePlatform.WINDOWSARM64 => "windows-arm64",
            JavaRuntimePlatform.WINDOWSX64 => "windows-x64",
            JavaRuntimePlatform.WINDOWSX86 => "windows-x86",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };

    public static string ToPlatformString(JavaRuntimePlatform type) =>
        type switch
        {
            JavaRuntimePlatform.WINDOWSARM64 => "windows",
            JavaRuntimePlatform.WINDOWSX64 => "windows",
            JavaRuntimePlatform.WINDOWSX86 => "windows",
            JavaRuntimePlatform.LINUX => "linux",
            JavaRuntimePlatform.LINUXI386 => "linux",
            JavaRuntimePlatform.MACOS => "osx",
            JavaRuntimePlatform.MACOSARM64 => "osx",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
