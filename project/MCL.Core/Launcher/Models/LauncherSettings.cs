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

using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Enums;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.Launcher.Models;

public class LauncherSettings
{
    public LauncherType LauncherType { get; set; } = LauncherType.RELEASE;
    public ClientType ClientType { get; set; } = ClientType.FABRIC;
    public FabricInstallerType FabricInstallerType { get; set; } = FabricInstallerType.INSTALL_CLIENT;
    public QuiltInstallerType QuiltInstallerType { get; set; } = QuiltInstallerType.INSTALL_CLIENT;
    public JavaRuntimeType JavaRuntimeType { get; set; } = JavaRuntimeType.JAVA_RUNTIME_GAMMA;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = JavaRuntimePlatform.WINDOWSX64;

    public LauncherSettings() { }

    public LauncherSettings(
        LauncherType launcherType,
        ClientType clientType,
        FabricInstallerType fabricInstallerType,
        QuiltInstallerType quiltInstallerType,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        LauncherType = launcherType;
        ClientType = clientType;
        FabricInstallerType = fabricInstallerType;
        QuiltInstallerType = quiltInstallerType;
        JavaRuntimeType = javaRuntimeType;
        JavaRuntimePlatform = javaRuntimePlatform;
    }
}
