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

namespace MCL.Core.ModLoaders.Fabric.Models;

public class FabricUrls
{
    public string VersionManifest { get; set; } = "https://meta.fabricmc.net/v2/versions";
    public string LoaderJar { get; set; } =
        "https://maven.fabricmc.net/net/fabricmc/fabric-loader/{0}/fabric-loader-{0}.jar";
    public string LoaderProfile { get; set; } = "https://meta.fabricmc.net/v2/versions/loader/{0}/{1}/profile/json";

    public string ApiLoaderName { get; set; } = "net.fabricmc:fabric-loader";
    public string ApiIntermediaryName { get; set; } = "net.fabricmc:intermediary";

    public FabricUrls() { }
}
