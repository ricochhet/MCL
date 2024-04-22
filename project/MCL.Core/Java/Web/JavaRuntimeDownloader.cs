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

using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Web;

public static class JavaRuntimeDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        JavaRuntimeType javaRuntimeType,
        JavaVersionDetails javaRuntimeFiles
    )
    {
        if (ObjectValidator<Dictionary<string, JavaRuntimeFile>>.IsNullOrEmpty(javaRuntimeFiles?.Files))
            return false;

        foreach ((string path, JavaRuntimeFile javaRuntimeFile) in javaRuntimeFiles?.Files ?? [])
        {
            if (ObjectValidator<JavaRuntimeFileDownloads>.IsNull(javaRuntimeFile?.Downloads))
                continue;

            if (javaRuntimeFile.Type == "file")
            {
                if (
                    ObjectValidator<string>.IsNullOrWhiteSpace(
                        [javaRuntimeFile.Downloads?.Raw?.URL, javaRuntimeFile.Downloads?.Raw?.SHA1]
                    )
                )
                    return false;

                if (
                    !await Request.DownloadSHA1(
                        javaRuntimeFile.Downloads.Raw.URL,
                        VFS.Combine(
                            JavaPathResolver.DownloadedJavaRuntimePath(
                                launcherPath,
                                JavaRuntimeTypeResolver.ToString(javaRuntimeType)
                            ),
                            path
                        ),
                        javaRuntimeFile.Downloads.Raw.SHA1
                    )
                )
                    return false;
            }
        }

        return true;
    }
}
