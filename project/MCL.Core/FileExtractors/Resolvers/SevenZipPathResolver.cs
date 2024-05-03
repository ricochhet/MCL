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

using MCL.Core.FileExtractors.Models;
using MCL.Core.Launcher.Providers;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation.Operators;

namespace MCL.Core.FileExtractors.Resolvers;

public static class SevenZipPathResolver
{
    /// <summary>
    /// Check if the SevenZip executable exists under the 'Data' directory.
    /// If it is not found, attempt to use it from PATH.
    /// </summary>
    public static string SevenZipPath(SevenZipSettings? sevenZipSettings)
    {
        string windowsExecutable = VFS.Combine(
            SettingsProvider.DataDirectory,
            "SevenZip",
            sevenZipSettings?.Executable + ".exe"
        );
        if (VFS.Exists(windowsExecutable))
            return windowsExecutable;
        NotificationProvider.Info("error.missing.7z");
        return sevenZipSettings?.Executable ?? StringOperator.Empty();
    }
}
