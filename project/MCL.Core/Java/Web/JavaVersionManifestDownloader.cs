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

using System.Text;
using System.Threading.Tasks;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.MiniCommon.Web;

namespace MCL.Core.Java.Web;

public static class JavaVersionManifestDownloader
{
    /// <summary>
    /// Download the Java version manifest.
    /// </summary>
    public static async Task<bool> Download(LauncherPath? launcherPath, MUrls? mUrls)
    {
        if (StringValidator.IsNullOrWhiteSpace([mUrls?.JavaVersionManifest]))
            return false;

        string filepath = JavaPathResolver.JavaVersionManifestPath(launcherPath);
        string? javaVersionManifest = await Request.GetJsonAsync<JavaVersionManifest>(
            mUrls!.JavaVersionManifest,
            filepath,
            Encoding.UTF8,
            JavaVersionManifestContext.Default
        );
        return StringValidator.IsNotNullOrWhiteSpace([javaVersionManifest]);
    }
}
