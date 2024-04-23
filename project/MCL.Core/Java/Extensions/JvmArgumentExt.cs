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

using System.Collections.Generic;
using System.Linq;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.Java.Extensions;

public static class JvmArgumentExt
{
    /// <summary>
    /// Compares ClientType 'A' to ClientType 'B' and adds argument if successful.
    /// </summary>
    public static void Add(
        this JvmArguments jvmArguments,
        ClientType a,
        ClientType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    /// <summary>
    /// Compares LauncherType 'A' to LauncherType 'B' and adds argument if successful.
    /// </summary>
    public static void Add(
        this JvmArguments jvmArguments,
        LauncherType a,
        LauncherType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    /// <summary>
    /// Compares FabricInstallerType 'A' to FabricInstallerType 'B' and adds argument if successful.
    /// </summary>
    public static void Add(
        this JvmArguments jvmArguments,
        FabricInstallerType a,
        FabricInstallerType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    /// <summary>
    /// Compares QuiltInstallerType 'A' to QuiltInstallerType 'B' and adds argument if successful.
    /// </summary>
    public static void Add(
        this JvmArguments jvmArguments,
        QuiltInstallerType a,
        QuiltInstallerType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    /// <summary>
    /// Combines two JvmArguments objects and keeps the last occurences of 'Arg'.
    /// </summary>
    public static JvmArguments Concat(this JvmArguments jvmArguments, JvmArguments concat)
    {
        List<LaunchArg> arguments = jvmArguments
            .Arguments.Concat(concat.Arguments)
            .GroupBy(arg => arg.Arg)
            .Select(group => group.Last())
            .ToList();

        return new JvmArguments { Arguments = arguments };
    }
}
