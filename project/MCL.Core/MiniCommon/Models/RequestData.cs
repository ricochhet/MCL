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

namespace MCL.Core.MiniCommon.Models;

public class RequestData
{
    public string URL { get; set; }
    public string FilePath { get; set; }
    public int Size { get; set; }
    public string SHA1 { get; set; }
    public TimeSpan Elapsed { get; set; }
    public static event Action<RequestData>? OnRequestDataAdded;

    public RequestData(string url, string filePath, int size, string sha1, TimeSpan elapsed)
    {
        URL = url;
        FilePath = filePath;
        Size = size;
        SHA1 = sha1;
        Elapsed = elapsed;
        OnRequestDataAdded?.Invoke(this);
    }
}
