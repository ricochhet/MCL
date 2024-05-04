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

using System.Diagnostics;

namespace MiniCommon.BuildInfo;

public static class AotConstants
{
#if NET8_0_OR_GREATER
    public static bool IsNativeAot { get; }

    static AotConstants()
    {
        StackTrace stackTrace = new(false);
#pragma warning disable IL2026
        IsNativeAot = stackTrace.GetFrame(0)?.GetMethod() is null;
#pragma warning restore IL2026
    }
#else
    // This is a compile-time const so that the irrelevant code is removed during compilation.
    public const bool IsNativeAot = false;
#endif
}
