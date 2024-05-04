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
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MCL.Core.Launcher.Resolvers;

public static class ClientTypeResolver
{
    /// <summary>
    /// Convert ClientType to a Java class name string.
    /// </summary>
    public static string ToString(ClientType? type, MainClassNames? mainClassNames) =>
        type switch
        {
            ClientType.VANILLA or ClientType.CUSTOM => mainClassNames?.Vanilla ?? Validate.For.EmptyString(),
            ClientType.FABRIC => mainClassNames?.Fabric ?? Validate.For.EmptyString(),
            ClientType.QUILT => mainClassNames?.Quilt ?? Validate.For.EmptyString(),
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
}
