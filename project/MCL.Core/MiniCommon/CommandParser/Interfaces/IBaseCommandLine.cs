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
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon.CommandParser;

public interface IBaseCommandLine
{
    public string Prefix { get; set; }
    public char[] Seperator { get; set; }

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with the argument's value of type T.
    /// </summary>
    public void ProcessArgument<T>(string[] args, Command command, Func<string, T?> converter, Action<T?> action);

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with a dictionary of key-value pairs extracted from the argument.
    /// </summary>
    public void ProcessArgument(string[] args, Command command, Action<Dictionary<string, string>> action);

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with the argument's value of type T.
    /// </summary>
    public Task ProcessArgumentAsync<T>(
        string[] args,
        Command command,
        Func<string, T?> converter,
        Func<T?, Task> action
    );

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with a dictionary of key-value pairs extracted from the argument.
    /// </summary>
    public Task ProcessArgumentAsync(
        string[] args,
        Command command,
        Func<Dictionary<string, string>, Task> action
    );

    /// <summary>
    /// Parses a string containing key-value pairs separated by specified separators into a dictionary.
    /// </summary>
    public Dictionary<string, string> ParseKeyValuePairs(string input);

    /// <summary>
    /// Logs an exception message along with stack trace information.
    /// </summary>
    public void LogException(Exception ex);

    /// <summary>
    /// Pauses the command line execution until the user presses the 'F' key.
    /// </summary>
    public void Pause();
}
