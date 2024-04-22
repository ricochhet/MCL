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

namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltUrls
{
    public string VersionManifest { get; set; } = "https://meta.quiltmc.org/v3/versions";
    public string LoaderJar { get; set; } =
        "https://maven.quiltmc.org/repository/release/org/quiltmc/quilt-loader/{0}/quilt-loader-{0}.jar";
    public string LoaderProfile { get; set; } = "https://meta.quiltmc.org/v3/versions/loader/{0}/{1}/profile/json";

    public string ApiLoaderName { get; set; } = "org.quiltmc:quilt-loader";
    public string ApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public QuiltUrls() { }
}
