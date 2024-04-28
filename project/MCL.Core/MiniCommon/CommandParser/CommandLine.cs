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
using System.Linq;
using System.Threading.Tasks;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.MiniCommon.CommandParser;

public static class CommandLine
{
    public static string Prefix { get; private set; } = "--";
    private static readonly char[] _separator = [',', ';'];

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with the argument's value of type T.
    /// </summary>
    public static void ProcessArgument<T>(string[] args, Command command, Func<string, T?> converter, Action<T?> action)
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1)
            {
                T? value = default;
                if (index + 1 < args.Length && !args[index + 1].StartsWith("--"))
                {
                    value = converter(args[index + 1]);
                }
                action(value);
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with a dictionary of key-value pairs extracted from the argument.
    /// </summary>
    public static void ProcessArgument(string[] args, Command command, Action<Dictionary<string, string>> action)
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1 && index + 1 < args.Length && !args[index + 1].StartsWith("--"))
            {
                Dictionary<string, string> options = ParseKeyValuePairs(args[index + 1]);
                if (
                    command
                        .Parameters.Where(a => !a.Optional)
                        .All(a => options.ContainsKey(a?.Name ?? ValidationShims.StringEmpty()))
                    || ObjectValidator<List<CommandParameter>>.IsNullOrEmpty(command.Parameters)
                )
                    action(options);
                else
                    NotificationService.Error("commandline.error", command.Usage());
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with the argument's value of type T.
    /// </summary>
    public static async Task ProcessArgumentAsync<T>(
        string[] args,
        Command command,
        Func<string, T?> converter,
        Func<T?, Task> action
    )
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1)
            {
                T? value = default;
                if (index + 1 < args.Length && !args[index + 1].StartsWith("--"))
                {
                    value = converter(args[index + 1]);
                }
                await action(value);
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with a dictionary of key-value pairs extracted from the argument.
    /// </summary>
    public static async Task ProcessArgumentAsync(
        string[] args,
        Command command,
        Func<Dictionary<string, string>, Task> action
    )
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1 && index + 1 < args.Length && !args[index + 1].StartsWith("--"))
            {
                Dictionary<string, string> options = ParseKeyValuePairs(args[index + 1]);
                if (
                    command
                        .Parameters.Where(a => !a.Optional)
                        .All(a => options.ContainsKey(a?.Name ?? ValidationShims.StringEmpty()))
                    || ObjectValidator<List<CommandParameter>>.IsNullOrEmpty(command.Parameters)
                )
                    await action(options);
                else
                    NotificationService.Error("commandline.error", command.Usage());
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Parses a string containing key-value pairs separated by specified separators into a dictionary.
    /// </summary>
    public static Dictionary<string, string> ParseKeyValuePairs(string input)
    {
        Dictionary<string, string> keyValuePairs = [];
        string[] pairs = input.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split('=');
            if (keyValue.Length == 2)
                keyValuePairs[keyValue[0]] = keyValue[1];
        }
        return keyValuePairs;
    }

    /// <summary>
    /// Logs an exception message along with stack trace information.
    /// </summary>
    private static void LogException(Exception ex)
    {
        NotificationService.Error(
            "log.stack.trace",
            ex.Message,
            ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
        );
    }

    /// <summary>
    /// Pauses the command line execution until the user presses the 'F' key.
    /// </summary>
    public static void Pause()
    {
        NotificationService.Info("commandline.exit", "F");

#pragma warning disable IDE0079
#pragma warning disable S108
        while (Console.ReadKey(intercept: true).Key != ConsoleKey.F) { }
#pragma warning restore IDE0079, S108
    }
}
