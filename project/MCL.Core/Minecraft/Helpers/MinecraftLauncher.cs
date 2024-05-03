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

using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.MiniCommon.Validation.Validators;

namespace MCL.Core.Minecraft.Helpers;

public static class MinecraftLauncher
{
    /// <summary>
    /// Launch the game process specified by the ClientType.
    /// </summary>
    public static void Launch(Settings settings, string javaHome)
    {
        if (ClassValidator.IsNull(settings))
            return;

        settings.Save(
            settings.LauncherSettings?.ClientType,
            MLaunchOptions.DefaultJvmArguments(settings)?.JvmArguments()
        );
        if (ClassValidator.IsNull(VersionHelper.GetVersionDetails(settings.LauncherPath, settings.LauncherVersion)))
        {
            return;
        }

        JavaLauncher.Launch(
            settings,
            settings.LauncherPath?.MPath ?? StringOperator.Empty(),
            settings.LauncherSettings?.ClientType,
            JavaVersionHelper.GetMVersionJava(
                settings.LauncherPath,
                settings.LauncherVersion,
                settings.LauncherSettings
            ),
            javaHome
        );
    }
}
