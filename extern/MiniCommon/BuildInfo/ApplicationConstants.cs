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
using System.Diagnostics;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MiniCommon.BuildInfo;

public static class ApplicationConstants
{
    public static Version CurrentVersion
    {
        get
        {
            if (CompileConstants.IsDebug)
                return Version.Parse("0.0.0.1");
            if (
                Validate.For.IsNullOrWhiteSpace(
                    [Process.GetCurrentProcess().MainModule!.FileVersionInfo.FileVersion]
                )
            )
            {
                return Version.TryParse(
                    Process.GetCurrentProcess().MainModule!.FileVersionInfo.FileVersion,
                    out Version? version
                )
                    ? version
                    : Version.Parse("0.0.0.0");
            }
            return Version.Parse("0.0.0.0");
        }
    }
}
