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
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MCL.Core.FileExtractors.Services;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;
using MCL.Core.Modding.Services;
using MCL.Launcher.Commands.Downloaders;
using MCL.Launcher.Commands.Launcher;
using MCL.Launcher.Commands.Modding;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.Launcher";
        Log.Add(new NativeLogger(NativeLogLevel.Info));
        Log.Add(new FileStreamLogger(SettingsService.LogFilePath));
        LocalizationService.Init(Language.ENGLISH);
        NotificationService.OnNotificationAdded(
            (Notification notification) => Log.Base(notification.LogLevel, notification.Message)
        );
        NotificationService.Info("log.initialized");
        SettingsService.Init();
        Settings settings = SettingsService.Load();
        if (ObjectValidator<Settings>.IsNull(settings))
            return;
        RequestDataService.OnRequestCompleted(
            (RequestData requestData) => NotificationService.Info("request.get.success", requestData.URL)
        );

        Request.JsonSerializerOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        Request.HttpClientTimeOut = TimeSpan.FromMinutes(1);
        Watermark.Draw(SettingsService.WatermarkText);

        SevenZipService.Init(settings.SevenZipSettings);
        ModdingService.Init(settings.LauncherPath, settings.ModSettings);

        if (args.Length <= 0)
            return;

        List<ILauncherCommand> commands = [];

        commands.Add(new DownloadJava());
        commands.Add(new DownloadMinecraft());
        commands.Add(new DownloadFabricInstaller());
        commands.Add(new DownloadFabricLoader());
        commands.Add(new DownloadQuiltInstaller());
        commands.Add(new DownloadQuiltLoader());
        commands.Add(new DownloadPaperServer());
        commands.Add(new LaunchMinecraft());
        commands.Add(new LaunchPaperServer());
        commands.Add(new DeployMods());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, settings);
    }
}
