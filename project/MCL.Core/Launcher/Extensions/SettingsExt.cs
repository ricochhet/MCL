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
using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.Modding.Extensions;
using MCL.Core.Modding.Models;

namespace MCL.Core.Launcher.Extensions;

public static class SettingsExt
{
    /// <summary>
    /// Get and set values to Settings object.
    /// </summary>
    public static void Set<T>(
        this Settings settings,
        Dictionary<string, string> options,
        string key,
        Func<string, T> converter,
        Func<Settings?, T?> propertySelector,
        Action<Settings, T> setter
    )
        where T : class
    {
        if (options.TryGetValue(key, out string? value) && ClassValidator.IsNotNull(propertySelector(settings)))
        {
            T convertedValue = converter(value);
            setter(settings, convertedValue);
        }
    }

    /// <summary>
    /// Save JvmArguments to Settings specified by the ClientType.
    /// </summary>
    public static Settings Save(this Settings settings, ClientType? clientType, JvmArguments? jvmArguments)
    {
        switch (clientType)
        {
            case ClientType.VANILLA
            or ClientType.CUSTOM:
                if (settings.MJvmArguments != jvmArguments)
                {
                    settings.MJvmArguments =
                        jvmArguments?.Concat(settings.OverrideMJvmArguments) ?? ClassOperator.Empty<JvmArguments>();
                    SettingsProvider.Save(settings);
                }
                break;
            case ClientType.FABRIC:
                if (settings.FabricJvmArguments != jvmArguments)
                {
                    settings.FabricJvmArguments =
                        jvmArguments?.Concat(settings.OverrideFabricJvmArguments)
                        ?? ClassOperator.Empty<JvmArguments>();
                    SettingsProvider.Save(settings);
                }
                break;
            case ClientType.QUILT:
                if (settings.QuiltJvmArguments != jvmArguments)
                {
                    settings.QuiltJvmArguments =
                        jvmArguments?.Concat(settings.OverrideQuiltJvmArguments) ?? ClassOperator.Empty<JvmArguments>();
                    SettingsProvider.Save(settings);
                }
                break;
        }

        return settings;
    }

    /// <summary>
    /// Save JvmArguments to Settings for 'PaperJvmArguments'.
    /// </summary>
    public static Settings Save(this Settings settings, JvmArguments? jvmArguments)
    {
        if (settings.PaperJvmArguments != jvmArguments)
        {
            settings.PaperJvmArguments =
                jvmArguments?.Concat(settings.OverridePaperJvmArguments) ?? ClassOperator.Empty<JvmArguments>();
            SettingsProvider.Save(settings);
        }
        return settings;
    }

    /// <summary>
    /// Save ModSettings to Settings for 'ModSettings'.
    /// </summary>
    public static Settings Save(this Settings settings, ModSettings? modSettings)
    {
        if (settings.ModSettings != modSettings)
        {
            settings.ModSettings =
                modSettings?.Concat(settings.OverrideModSettings) ?? ClassOperator.Empty<ModSettings>();
            SettingsProvider.Save(settings);
        }
        return settings;
    }

    /// <summary>
    /// Save LauncherInstance to Settings for 'LauncherInstance'.
    /// </summary>
    public static Settings Save(this Settings settings, LauncherInstance? launcherInstance)
    {
        if (settings.LauncherInstance != launcherInstance)
        {
            settings.LauncherInstance =
                launcherInstance?.Concat(settings.OverrideLauncherInstance) ?? ClassOperator.Empty<LauncherInstance>();
            SettingsProvider.Save(settings);
        }
        return settings;
    }
}
