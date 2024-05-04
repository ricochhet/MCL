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

using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MiniCommon.IO;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Minecraft.Helpers;

public static class AssetHelper
{
    /// <summary>
    /// Get the asset data identifier for the specified MVersion.
    /// </summary>
    public static string GetAssetId(LauncherPath? launcherPath, LauncherVersion? launcherVersion)
    {
        if (StringValidator.IsNullOrWhiteSpace([launcherVersion?.MVersion]))
            return string.Empty;

        MVersionManifest? versionManifest = Json.Load<MVersionManifest>(
            MPathResolver.VersionManifestPath(launcherPath),
            MVersionManifestContext.Default
        );

        if (ClassValidator.IsNull(versionManifest))
            return string.Empty;

        MVersion? version = VersionHelper.GetVersion(launcherVersion, versionManifest);
        if (ClassValidator.IsNull(version))
            return string.Empty;

        MVersionDetails? versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(launcherPath, version),
            MVersionDetailsContext.Default
        );

        return StringValidator.IsNotNullOrWhiteSpace([versionDetails?.Assets]) ? versionDetails!.Assets! : string.Empty;
    }
}
