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

public static class JavaRuntimeTypeResolver
{
    /// <summary>
    /// Convert JavaRuntimeType into a direct string.
    /// </summary>
    public static string ToString(JavaRuntimeType? type) =>
        type switch
        {
            JavaRuntimeType.JAVA_RUNTIME_ALPHA => "java-runtime-alpha",
            JavaRuntimeType.JAVA_RUNTIME_BETA => "java-runtime-beta",
            JavaRuntimeType.JAVA_RUNTIME_DELTA => "java-runtime-gamma",
            JavaRuntimeType.JAVA_RUNTIME_GAMMA => "java-runtime-gamma",
            JavaRuntimeType.JAVA_RUNTIME_GAMMA_SNAPSHOT => "java-runtime-gamma-snapshot",
            JavaRuntimeType.JRE_LEGACY => "jre-legacy",
            JavaRuntimeType.MINECRAFT_JAVA_EXE => "minecraft-java-exe",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
