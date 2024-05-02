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
    public static MOption[]? DefaultJvmArguments(Settings settings)
    {
        if (!VersionHelper.VersionExists(settings))
            return null;

        return
        [
            new MOption
            {
                Arg = "-Xms{0}m",
                ArgParams = [settings.LauncherMemory?.MemoryMinMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "-Xmx{0}m",
                ArgParams = [settings.LauncherMemory?.MemoryMaxMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "-Dgraal.LoopRotation={0}",
                ArgParams = ["true"],
                Condition = settings.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption
            {
                Arg = "-Dgraal.PartialUnroll={0}",
                ArgParams = ["true"],
                Condition = settings.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption
            {
                Arg = "-Dgraal.VectorizeSIMD={0}",
                ArgParams = ["true"],
                Condition = settings.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption { Arg = "-XX:+UnlockExperimentalVMOptions" },
            new MOption { Arg = "-XX:+UseG1GC" },
            new MOption { Arg = "-XX:G1NewSizePercent={0}", ArgParams = ["20"] },
            new MOption { Arg = "-XX:G1ReservePercent={0}", ArgParams = ["20"] },
            new MOption { Arg = "-XX:MaxGCPauseMillis={0}", ArgParams = ["50"] },
            new MOption { Arg = "-XX:G1HeapRegionSize={0}", ArgParams = ["32M"] },
            new MOption
            {
                Arg = "-XX:HeapDumpPath={0}",
                ArgParams = ["MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump"]
            },
            new MOption
            {
                Arg = "-Djava.library.path={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MOption
            {
                Arg = "-Dminecraft.client.jar={0}",
                ArgParams = [MPathResolver.ClientLibrary(settings.LauncherVersion)]
            },
            new MOption
            {
                Arg = "-Djna.tmpdir={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MOption
            {
                Arg = "-Dorg.lwjgl.system.SharedLibraryExtractPath={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MOption
            {
                Arg = "-Dio.netty.native.workdir={0}",
                ArgParams = [MPathResolver.NativesLibraries(settings.LauncherVersion)]
            },
            new MOption
            {
                Arg = "-Dlog4j2.configurationFile={0}",
                ArgParams = [MPathResolver.LoggingPath(settings.LauncherVersion)],
                Condition = settings.LauncherSettings?.LauncherType == LauncherType.DEBUG
            },
            new MOption
            {
                Arg = "-DMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.VANILLA
            },
            new MOption
            {
                Arg = "-DFabricMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.FABRIC
            },
            new MOption
            {
                Arg = "-DFabricMcEmu={0}",
                ArgParams = [ClientTypeResolver.ToString(ClientType.VANILLA, settings.MainClassNames)],
                Condition = settings.LauncherSettings?.ClientType == ClientType.QUILT
            },
            new MOption { Arg = "-Dlog4j2.formatMsgNoLookups={0}", ArgParams = ["true"] },
            new MOption { Arg = "-Djava.rmi.server.useCodebaseOnly={0}", ArgParams = ["true"] },
            new MOption { Arg = "-Dcom.sun.jndi.rmi.object.trustURLCodebase={0}", ArgParams = ["false"] },
            new MOption
            {
                Arg = "-Dminecraft.launcher.brand={0}",
                ArgParams = [settings.LauncherVersion?.Brand ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "-Dminecraft.launcher.version={0}",
                ArgParams = [settings.LauncherVersion?.Version ?? ValidationShims.StringEmpty()]
            },
            new MOption
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
            new MOption
            {
                Arg = "--username {0}",
                ArgParams = [settings.LauncherUsername?.ValidateUsername() ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "--userType {0}",
                ArgParams = [settings.LauncherUsername?.UserType ?? ValidationShims.StringEmpty()]
            },
            new MOption { Arg = "--gameDir {0}", ArgParams = ["."] },
            new MOption
            {
                Arg = "--assetIndex {0}",
                ArgParams = [AssetHelper.GetAssetId(settings.LauncherPath, settings.LauncherVersion)]
            },
            new MOption { Arg = "--assetsDir {0}", ArgParams = [MPathResolver.BaseAssetsPath] },
            new MOption
            {
                Arg = "--accessToken {0}",
                ArgParams = [settings.LauncherUsername?.AccessToken ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "--uuid {0}",
                ArgParams = [settings.LauncherUsername?.UUID() ?? ValidationShims.StringEmpty()],
                Condition = settings.LauncherSettings?.AuthType == AuthType.ONLINE
            },
            new MOption
            {
                Arg = "--uuid {0}",
                ArgParams = [settings.LauncherUsername?.OfflineUUID() ?? ValidationShims.StringEmpty()],
                Condition = settings.LauncherSettings?.AuthType == AuthType.OFFLINE
            },
            new MOption { Arg = "--clientId {0}", ArgParams = ["0"] },
            new MOption { Arg = "--xuid {0}", ArgParams = ["0"] },
            new MOption
            {
                Arg = "--version {0}",
                ArgParams = [settings.LauncherVersion?.MVersion ?? ValidationShims.StringEmpty()]
            },
            new MOption
            {
                Arg = "--versionType \"{0}\"",
                ArgParams = [$"{settings.LauncherVersion?.Brand} {settings.LauncherVersion?.Version}"]
            },
        ];
    }
}
