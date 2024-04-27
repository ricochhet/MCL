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

using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Resolvers;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Helpers;

public static class LaunchArgs
{
    /// <summary>
    /// The default JvmArguments to launch the process with.
    /// </summary>
    public static JvmArguments? DefaultJvmArguments(Settings settings)
    {
        if (ObjectValidator<Settings>.IsNull(settings))
            return null;

        JvmArguments jvmArguments = new();
        string libraries = MPathResolver.NativesLibraries(settings.LauncherVersion);

        jvmArguments.Add(
            "-Xms{0}m",
            [settings.LauncherMemory?.MemoryMinMb.ToString() ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add(
            "-Xmx{0}m",
            [settings.LauncherMemory?.MemoryMaxMb.ToString() ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add("-XX:+UnlockExperimentalVMOptions");
        jvmArguments.Add("-XX:+UseG1GC");
        jvmArguments.Add("-XX:G1NewSizePercent=20");
        jvmArguments.Add("-XX:G1ReservePercent=20");
        jvmArguments.Add("-XX:MaxGCPauseMillis=50");
        jvmArguments.Add("-XX:G1HeapRegionSize=32M");
        jvmArguments.Add("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
        jvmArguments.Add("-Djava.library.path={0}", [libraries]);
        jvmArguments.Add("-Dminecraft.client.jar={0}", [MPathResolver.ClientLibrary(settings.LauncherVersion)]);
        jvmArguments.Add("-Djna.tmpdir={0}", [libraries]);
        jvmArguments.Add("-Dorg.lwjgl.system.SharedLibraryExtractPath={0}", [libraries]);
        jvmArguments.Add("-Dio.netty.native.workdir={0}", [libraries]);
        jvmArguments.Add(
            settings.LauncherSettings?.LauncherType ?? LauncherType.RELEASE,
            LauncherType.DEBUG,
            "-Dlog4j2.configurationFile={0}",
            [MPathResolver.LoggingPath(settings.LauncherVersion)]
        );
        jvmArguments.Add(
            settings.LauncherSettings?.ClientType ?? ClientType.VANILLA,
            ClientType.VANILLA,
            "-DMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)]
        );
        jvmArguments.Add(
            settings.LauncherSettings?.ClientType ?? ClientType.VANILLA,
            ClientType.FABRIC,
            "-DFabricMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)]
        );
        jvmArguments.Add(
            settings.LauncherSettings?.ClientType ?? ClientType.VANILLA,
            ClientType.QUILT,
            "-DFabricMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)]
        );
        jvmArguments.Add("-Dlog4j2.formatMsgNoLookups=true");
        jvmArguments.Add("-Djava.rmi.server.useCodebaseOnly=true");
        jvmArguments.Add("-Dcom.sun.jndi.rmi.object.trustURLCodebase=false");
        jvmArguments.Add(
            "-Dminecraft.launcher.brand={0}",
            [settings.LauncherVersion?.Brand ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add(
            "-Dminecraft.launcher.version={0}",
            [settings.LauncherVersion?.Version ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add(
            "-cp {0} {1}",
            [
                ClassPathHelper.GetClassLibraries(
                    settings.LauncherVersion,
                    settings.LauncherInstance,
                    settings.LauncherSettings
                ),
                ClientTypeResolver.ToString(settings.LauncherSettings?.ClientType, settings.MainClassNames)
            ]
        );
        jvmArguments.Add(
            "--username {0}",
            [settings.LauncherUsername?.ValidateUsername() ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add("--userType {0}", [settings.LauncherUsername?.UserType ?? ValidationShims.StringEmpty()]);
        jvmArguments.Add("--gameDir {0}", ["."]);
        jvmArguments.Add("--assetIndex {0}", [AssetHelper.GetAssetId(settings.LauncherPath, settings.LauncherVersion)]);
        jvmArguments.Add("--assetsDir {0}", [MPathResolver.BaseAssetsPath]);
        jvmArguments.Add(
            "--accessToken {0}",
            [settings.LauncherUsername?.AccessToken ?? ValidationShims.StringEmpty()]
        );
        jvmArguments.Add("--uuid {0}", [settings.LauncherUsername?.UUID() ?? ValidationShims.StringEmpty()]);
        jvmArguments.Add("--clientId {0}", ["0"]);
        jvmArguments.Add("--xuid {0}", ["0"]);
        jvmArguments.Add("--version {0}", [settings.LauncherVersion?.MVersion ?? ValidationShims.StringEmpty()]);
        jvmArguments.Add(
            "--versionType {0}",
            [$"{settings.LauncherVersion?.Brand} {settings.LauncherVersion?.Version}"]
        );

        return jvmArguments;
    }
}
