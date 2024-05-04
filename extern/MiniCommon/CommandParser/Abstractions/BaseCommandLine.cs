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
using MiniCommon.CommandParser.Interfaces;
using MiniCommon.Extensions;
using MiniCommon.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MiniCommon.CommandParser.Abstractions;

public class BaseCommandLine : IBaseCommandLine
{
    public virtual string Prefix { get; set; } = "--";

    /// <summary>
    /// Processes a command line argument identified by a flag and invokes the provided action with the argument's value of type T.
    /// </summary>
    public virtual void ProcessArgument<T>(
        string[] args,
        Command command,
        Func<string, T?> converter,
        Action<T?> action
    )
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1)
            {
                T? value = default;
                if (index + 1 < args.Length && !args[index + 1].StartsWith(Prefix))
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
    public virtual void ProcessArgument(string[] args, Command command, Action<Dictionary<string, string>> action)
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1)
            {
                if (args.Length <= index + 1)
                {
                    action([]);
                }
                else if (index + 1 < args.Length && !args[index + 1].StartsWith(Prefix))
                {
                    Dictionary<string, string> options = args[index + 1].ParseKeyValuePairs();
                    if (
                        command
                            .Parameters.Where(a => !a.Optional)
                            .All(a => options.ContainsKey(a?.Name ?? Validate.For.EmptyString()))
                    )
                    {
                        action(options);
                    }
                    else
                    {
                        NotificationProvider.Error("commandline.error", command.Usage());
                    }
                }
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
    public virtual async Task ProcessArgumentAsync<T>(
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
                if (index + 1 < args.Length && !args[index + 1].StartsWith(Prefix))
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
    public virtual async Task ProcessArgumentAsync(
        string[] args,
        Command command,
        Func<Dictionary<string, string>, Task> action
    )
    {
        try
        {
            int index = Array.IndexOf(args, Prefix + command.Name);
            if (index != -1)
            {
                if (args.Length <= index + 1)
                {
                    await action([]);
                }
                else if (index + 1 < args.Length && !args[index + 1].StartsWith(Prefix))
                {
                    Dictionary<string, string> options = args[index + 1].ParseKeyValuePairs();
                    if (
                        command
                            .Parameters.Where(a => !a.Optional)
                            .All(a => options.ContainsKey(a?.Name ?? Validate.For.EmptyString()))
                    )
                    {
                        await action(options);
                    }
                    else
                    {
                        NotificationProvider.Error("commandline.error", command.Usage());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Logs an exception message along with stack trace information.
    /// </summary>
    public virtual void LogException(Exception ex)
    {
        NotificationProvider.Error(
            "log.stack.trace",
            ex.Message,
            ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
        );
    }

    /// <summary>
    /// Pauses the command line execution until the user presses the 'F' key.
    /// </summary>
    public virtual void Pause()
    {
        NotificationProvider.Info("commandline.exit", "F");

#pragma warning disable S108
        while (Console.ReadKey(intercept: true).Key != ConsoleKey.F) { }
#pragma warning restore S108
    }
}
