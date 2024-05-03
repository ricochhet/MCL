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
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperLaunchOptions
{
    /// <summary>
    /// The default JvmArguments to launch the process with.
    /// </summary>
    public static MOption[]? DefaultJvmArguments(Settings settings)
    {
        if (!PaperVersionHelper.VersionExists(settings))
            return null;

        return
        [
            new MOption
            {
                Arg = "-Xms{0}m",
                ArgParams = [settings!?.LauncherMemory?.MemoryMinMb.ToString() ?? StringOperator.Empty()]
            },
            new MOption
            {
                Arg = "-Xmx{0}m",
                ArgParams = [settings!?.LauncherMemory?.MemoryMaxMb.ToString() ?? StringOperator.Empty()]
            },
            new MOption
            {
                Arg = "-Dgraal.LoopRotation={0}",
                ArgParams = ["true"],
                Condition = settings!?.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption
            {
                Arg = "-Dgraal.PartialUnroll={0}",
                ArgParams = ["true"],
                Condition = settings!?.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption
            {
                Arg = "-Dgraal.VectorizeSIMD={0}",
                ArgParams = ["true"],
                Condition = settings!?.LauncherSettings?.JvmType == JvmArgumentType.GRAAL_VM
            },
            new MOption { Arg = "-XX:+AlwaysPreTouch" },
            new MOption { Arg = "-XX:+DisableExplicitGC" },
            new MOption { Arg = "-XX:+ParallelRefProcEnabled" },
            new MOption { Arg = "-XX:+PerfDisableSharedMem" },
            new MOption { Arg = "-XX:+UnlockExperimentalVMOptions" },
            new MOption { Arg = "-XX:+UseG1GC" },
            new MOption { Arg = "-XX:G1HeapRegionSize={0}", ArgParams = ["8M"] },
            new MOption { Arg = "-XX:G1HeapWastePercent={0}", ArgParams = ["5"] },
            new MOption { Arg = "-XX:G1MaxNewSizePercent={0}", ArgParams = ["40"] },
            new MOption { Arg = "-XX:G1MixedGCCountTarget={0}", ArgParams = ["4"] },
            new MOption { Arg = "-XX:G1MixedGCLiveThresholdPercent={0}", ArgParams = ["90"] },
            new MOption { Arg = "-XX:G1NewSizePercent={0}", ArgParams = ["30"] },
            new MOption { Arg = "-XX:G1RSetUpdatingPauseTimePercent={0}", ArgParams = ["5"] },
            new MOption { Arg = "-XX:G1ReservePercent={0}", ArgParams = ["20"] },
            new MOption { Arg = "-XX:InitiatingHeapOccupancyPercent={0}", ArgParams = ["15"] },
            new MOption { Arg = "-XX:MaxGCPauseMillis={0}", ArgParams = ["200"] },
            new MOption { Arg = "-XX:MaxTenuringThreshold={0}", ArgParams = ["1"] },
            new MOption { Arg = "-XX:SurvivorRatio={0}", ArgParams = ["32"] },
            new MOption { Arg = "-Dusing.aikars.flags={0}", ArgParams = ["https://mcflags.emc.gs"] },
            new MOption { Arg = "-Daikars.new.flags={0}", ArgParams = ["true"] },
            new MOption
            {
                Arg = "-jar \"{0}\" {1}",
                ArgParams = [PaperPathResolver.JarName(settings!?.LauncherVersion), "nogui"]
            },
        ];
    }
}
