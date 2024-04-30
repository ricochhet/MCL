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
using MCL.Core.FileExtractors.Services;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon.Enums;
using MCL.Core.MiniCommon.Logger;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Web;
using MCL.Core.Modding.Services;

namespace MCL.Core.Managers;

public static class ServiceManager
{
    public static Settings? Settings { get; private set; }

    public static Task<bool> Init()
    {
        try
        {
            LocalizationService.Init(SettingsService.LocalizationPath, Language.ENGLISH);
            NotificationService.OnNotificationAdded(
                (Notification notification) => Log.Base(notification.LogLevel, notification.Message)
            );
            NotificationService.Info("log.initialized");
            SettingsService.Init();
            Settings = SettingsService.Load();
            if (ObjectValidator<Settings>.IsNull(Settings))
                return Task.FromResult(false);
            RequestDataService.OnRequestCompleted(
                (RequestData requestData) => NotificationService.Info("request.get.success", requestData.URL)
            );
            Request.HttpRequest.HttpClientTimeOut = TimeSpan.FromMinutes(1);
            SevenZipService.Init(Settings!?.SevenZipSettings);
            ModdingService.Init(Settings!?.LauncherPath, Settings!?.ModSettings);
            Watermark.Draw(SettingsService.WatermarkText);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Log.Base(NativeLogLevel.Fatal, ex.ToString());
            return Task.FromResult(false);
        }
    }
}
