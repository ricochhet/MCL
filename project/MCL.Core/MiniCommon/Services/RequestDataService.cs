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
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon.Services;

public static class RequestDataService
{
    private static readonly List<RequestData> _requests = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(RequestData item) => _requests.Add(item);

    public static void Add(string url, string filePath, int size, string sha1) =>
        _requests.Add(new(url, filePath, size, sha1));

    public static void Clear() => _requests.Clear();

    public static void OnRequestCompleted(Action<RequestData> func)
    {
        RequestData.OnRequestDataAdded += func;
        RequestData.OnRequestDataAdded += Manage;
    }

    private static void Manage(RequestData _)
    {
        if (_requests.Count > MaxSize)
            _requests.RemoveAt(0);
    }
}
