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

using System.Threading.Tasks;

namespace MCL.Core.ModLoaders.Interfaces.Services;

public interface IModLoaderLoaderDownloadService
{
    /// <summary>
    /// Download all parts of the loader.
    /// </summary>
    public abstract Task<bool> Download(bool loadLocalVersionManifest = false, bool loadLocalVersionDetails = false);

    /// <summary>
    /// Download the version manifest.
    /// </summary>
    public abstract Task<bool> DownloadVersionManifest();

    /// <summary>
    /// Load the version manifest from the download path.
    /// </summary>
    public abstract bool LoadVersionManifest();

    /// <summary>
    /// Load the version manifest from the download path, without logging errors if loading failed.
    /// </summary>
    public abstract bool LoadVersionManifestWithoutLogging();

    /// <summary>
    /// Exclusively download the profile.
    /// </summary>
    public abstract Task<bool> DownloadProfile();

    /// <summary>
    /// Load the profile from the download path.
    /// </summary>
    public abstract bool LoadProfile();

    /// <summary>
    /// Load the loader version specified by the LoaderVersion from the VersionManifest download path.
    /// </summary>
    public abstract bool LoadLoaderVersion();

    /// <summary>
    /// Download the loader jar.
    /// </summary>
    public abstract Task<bool> DownloadLoader();
}
