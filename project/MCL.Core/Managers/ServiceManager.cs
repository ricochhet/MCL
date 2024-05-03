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
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Providers;
using MCL.Core.MiniCommon.Enums;
using MCL.Core.MiniCommon.Logger;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.MiniCommon.Web;

namespace MCL.Core.Managers;

public static class ServiceManager
{
    public static Settings? Settings { get; private set; }

    public static Task<bool> Init()
    {
        try
        {
            LocalizationProvider.Init(SettingsProvider.LocalizationPath, Language.ENGLISH);
            NotificationProvider.OnNotificationAdded(
                (Notification notification) => Log.Base(notification.LogLevel, notification.Message)
            );
            NotificationProvider.Info("log.initialized");
            SettingsProvider.FirstRun();
            Settings = SettingsProvider.Load();
            if (ClassValidator.IsNull(Settings))
                return Task.FromResult(false);
            RequestDataProvider.OnRequestCompleted(
                (RequestData requestData) =>
                    NotificationProvider.Info("request.get.success", requestData.URL, requestData.Elapsed.ToString("c"))
            );
            Request.HttpRequest.HttpClientTimeOut = TimeSpan.FromMinutes(1);
            Watermark.Draw(SettingsProvider.WatermarkText);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.ToString());
            return Task.FromResult(false);
        }
    }
}
