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
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MCL.Core.Launcher.Models;

public class MOption
{
    public string? Arg { get; set; }
    public string[]? ArgParams { get; set; }
    public int Priority { get; set; }
    public bool Ignore { get; set; }
    public bool Condition { get; set; } = true;

    public MOption() { }

    public MOption(string arg, string[]? argParams = null, int priority = 0, bool ignore = false)
    {
        Arg = arg;
        ArgParams = argParams ?? [];
        Priority = priority;
        Ignore = ignore;
    }

    public string? Parse()
    {
        if (ArgParams == null || ArgParams.Length == 0)
            return Arg;

        return string.Format(Arg ?? Validate.For.EmptyString(), ArgParams);
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MOption))]
internal partial class MOptionContext : JsonSerializerContext;
