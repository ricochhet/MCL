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
using MCL.Core.Launcher.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperLauncher
{
    /// <summary>
    /// Launch the Paper server process.
    /// </summary>
    public static void Launch(Settings? settings, string javaHome)
    {
        if (Validate.For.IsNull(settings))
            return;

        settings!.Save(PaperLaunchOptions.DefaultJvmArguments(settings!)?.JvmArguments());
        JavaLauncher.Launch(
            settings,
            PaperPathResolver.InstallerPath(settings!?.LauncherPath, settings!?.LauncherVersion),
            settings!?.PaperJvmArguments,
            settings!?.LauncherSettings?.JavaRuntimeType,
            javaHome
        );
    }
}
