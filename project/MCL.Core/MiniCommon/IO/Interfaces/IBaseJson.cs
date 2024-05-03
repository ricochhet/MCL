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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MCL.Core.MiniCommon.IO.Interfaces;

public interface IBaseJson
{
    /// <summary>
    /// Serialize data of type T.
    /// </summary>
    public abstract string Serialize<T>(T data, JsonSerializerOptions options);

    /// <summary>
    /// Serialize data of type T from serializer context.
    /// </summary>
    public abstract string Serialize<T>(T data, JsonSerializerContext ctx);

    /// <summary>
    /// Deserialize data of type T.
    /// </summary>
    public abstract T? Deserialize<T>(string json, JsonSerializerOptions options);

    /// <summary>
    /// Deserialize data of type T from serializer context.
    /// </summary>
    public abstract T? Deserialize<T>(string data, JsonSerializerContext ctx)
        where T : class;

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>
    public abstract void Save<T>(string filepath, T data, JsonSerializerOptions options);

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>
    public abstract void Save<T>(string filepath, T data, JsonSerializerContext ctx);

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public abstract T? Load<T>(string filepath, JsonSerializerOptions options)
        where T : new();

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public abstract T? Load<T>(string filepath, JsonSerializerContext ctx)
        where T : class;
}
