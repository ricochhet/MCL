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
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon.Models;

public class Notification
{
    public DateTime CurrentDateTime { get; set; } = DateTime.Now;
    public NativeLogLevel LogLevel { get; set; }
    public string ID { get; set; }
    public string Message { get; set; }
    public string[] Params { get; set; }
    public static event Action<Notification> OnNotificationAdded;
#nullable enable
    public Exception? Exception { get; set; }

    public Notification(NativeLogLevel logLevel, string id, Exception? exception = null)
    {
        LogLevel = logLevel;
        ID = id;
        Message = LocalizationService.Translate(ID);
        Exception = exception;
        OnNotificationAdded?.Invoke(this);
    }

    public Notification(NativeLogLevel logLevel, string id, string[] _params, Exception? exception = null)
#nullable disable
    {
        LogLevel = logLevel;
        ID = id;
        Params = _params;
        Message = LocalizationService.FormatTranslate(ID, Params);
        Exception = exception;
        OnNotificationAdded?.Invoke(this);
    }
}