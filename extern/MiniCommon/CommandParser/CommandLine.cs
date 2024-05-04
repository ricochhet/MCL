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
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniCommon.CommandParser.Abstractions;
using MiniCommon.CommandParser.Interfaces;
using MiniCommon.Models;

namespace MiniCommon.CommandParser;

public class CommandLine : ICommandLine
{
    public static BaseCommandLine BaseCommandLine { get; } = new();

    /// <inheritdoc />
    public static void ProcessArgument<T>(string[] args, Command command, Func<string, T?> converter, Action<T?> action)
    {
        BaseCommandLine.ProcessArgument(args, command, converter, action);
    }

    /// <inheritdoc />
    public static void ProcessArgument(string[] args, Command command, Action<Dictionary<string, string>> action)
    {
        BaseCommandLine.ProcessArgument(args, command, action);
    }

    /// <inheritdoc />
    public static async Task ProcessArgumentAsync<T>(
        string[] args,
        Command command,
        Func<string, T?> converter,
        Func<T?, Task> action
    )
    {
        await BaseCommandLine.ProcessArgumentAsync(args, command, converter, action);
    }

    /// <inheritdoc />
    public static async Task ProcessArgumentAsync(
        string[] args,
        Command command,
        Func<Dictionary<string, string>, Task> action
    )
    {
        await BaseCommandLine.ProcessArgumentAsync(args, command, action);
    }

    /// <inheritdoc />
    public static void Pause()
    {
        BaseCommandLine.Pause();
    }
}
