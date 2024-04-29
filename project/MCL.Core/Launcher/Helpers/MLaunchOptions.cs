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

using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Resolvers;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Launcher.Helpers;

public static class MLaunchOptions
{
    /// <summary>
    /// The default JvmArguments to launch the process with.
    /// </summary>
    public static MArgument[]? DefaultJvmArguments(Settings settings)
    {
        if (ObjectValidator<Settings>.IsNull(settings))
            return null;

        return
        [
            new MArgument
            {
                Arg = "-Xms{0}m",
                ArgParams = [settings.LauncherMemory?.MemoryMinMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "-Xmx{0}m",
                ArgParams = [settings.LauncherMemory?.MemoryMaxMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MArgument { Arg = "-XX:+UnlockExperimentalVMOptions" },
            new MArgument { Arg = "-XX:+UseG1GC" },
            new MArgument { Arg = "-XX:G1NewSizePercent=20" },
            new MArgument { Arg = "-XX:G1ReservePercent=20" },
            new MArgument { Arg = "-XX:MaxGCPauseMillis=50" },
            new MArgument { Arg = "-XX:G1HeapRegionSize=32M" },
            new MArgument
            {
                Arg = "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump"
            },
            new MArgument
            {
                Arg = "-Djava.library.path={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MArgument
            {
                Arg = "-Dminecraft.client.jar={0}",
                ArgParams = [MPathResolver.ClientLibrary(settings.LauncherVersion)]
            },
            new MArgument
            {
                Arg = "-Djna.tmpdir={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MArgument
            {
                Arg = "-Dorg.lwjgl.system.SharedLibraryExtractPath={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MArgument
            {
                Arg = "-Dio.netty.native.workdir={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MArgument
            {
                Arg = "-Dlog4j2.configurationFile={0}",
                ArgParams = [MPathResolver.LoggingPath(settings.LauncherVersion)],
                Condition = settings.LauncherSettings?.LauncherType == LauncherType.DEBUG
            },
            new MArgument
            {
                Arg = "-DMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.VANILLA
            },
            new MArgument
            {
                Arg = "-DFabricMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.FABRIC
            },
            new MArgument
            {
                Arg = "-DFabricMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.QUILT
            },
            new MArgument { Arg = "-Dlog4j2.formatMsgNoLookups=true" },
            new MArgument { Arg = "-Djava.rmi.server.useCodebaseOnly=true" },
            new MArgument { Arg = "-Dcom.sun.jndi.rmi.object.trustURLCodebase=false" },
            new MArgument
            {
                Arg = "-Dminecraft.launcher.brand={0}",
                ArgParams = [settings.LauncherVersion?.Brand ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "-Dminecraft.launcher.version={0}",
                ArgParams = [settings.LauncherVersion?.Version ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "-cp {0} {1}",
                ArgParams =
                [
                    ClassPathHelper.GetClassLibraries(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        settings.LauncherInstance,
                        settings.LauncherSettings
                    ),
                    ClientTypeResolver.ToString(settings.LauncherSettings?.ClientType, settings.MainClassNames)
                ]
            },
            new MArgument
            {
                Arg = "--username {0}",
                ArgParams = [settings.LauncherUsername?.ValidateUsername() ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "--userType {0}",
                ArgParams = [settings.LauncherUsername?.UserType ?? ValidationShims.StringEmpty()]
            },
            new MArgument { Arg = "--gameDir {0}", ArgParams = ["."] },
            new MArgument
            {
                Arg = "--assetIndex {0}",
                ArgParams = [AssetHelper.GetAssetId(settings.LauncherPath, settings.LauncherVersion)]
            },
            new MArgument { Arg = "--assetsDir {0}", ArgParams = [MPathResolver.BaseAssetsPath] },
            new MArgument
            {
                Arg = "--accessToken {0}",
                ArgParams = [settings.LauncherUsername?.AccessToken ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "--uuid {0}",
                ArgParams = [settings.LauncherUsername?.UUID() ?? ValidationShims.StringEmpty()],
                Condition = settings.LauncherSettings?.AuthType == AuthType.ONLINE
            },
            new MArgument
            {
                Arg = "--uuid {0}",
                ArgParams = [settings.LauncherUsername?.OfflineUUID() ?? ValidationShims.StringEmpty()],
                Condition = settings.LauncherSettings?.AuthType == AuthType.OFFLINE
            },
            new MArgument { Arg = "--clientId {0}", ArgParams = ["0"] },
            new MArgument { Arg = "--xuid {0}", ArgParams = ["0"] },
            new MArgument
            {
                Arg = "--version {0}",
                ArgParams = [settings.LauncherVersion?.MVersion ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "--versionType {0}",
                ArgParams = [$"{settings.LauncherVersion?.Brand} {settings.LauncherVersion?.Version}"]
            },
        ];
    }
}
