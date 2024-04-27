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
using MCL.Core.FileExtractors.Models;
using MCL.Core.FileExtractors.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Helpers;

namespace MCL.Core.FileExtractors.Services;

public static class SevenZipService
{
    private static SevenZipSettings? SevenZipSettings { get; set; }

    /// <summary>
    /// Initialize the SevenZip service.
    /// </summary>
    public static void Init(SevenZipSettings? sevenZipSettings)
    {
        SevenZipSettings = sevenZipSettings;
    }

    /// <summary>
    /// Extract an archive using SevenZip, using arguments specified by SevenZipSettings.
    /// </summary>
    public static void Extract(string source, string destination)
    {
        ProcessHelper.RunProcess(
            SevenZipPathResolver.SevenZipPath(SevenZipSettings),
            string.Format(SevenZipSettings?.ExtractArguments ?? ValidationShims.StringEmpty(), source, destination),
            Environment.CurrentDirectory,
            false
        );
    }
}
