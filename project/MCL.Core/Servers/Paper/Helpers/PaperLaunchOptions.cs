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

using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperLaunchOptions
{
    /// <summary>
    /// The default JvmArguments to launch the process with.
    /// </summary>
    public static MArgument[]? DefaultJvmArguments(Settings settings)
    {
        if (!PaperVersionHelper.VersionExists(settings))
            return null;

        return
        [
            new MArgument
            {
                Arg = "-Xms{0}m",
                ArgParams = [settings!?.LauncherMemory?.MemoryMinMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "-Xmx{0}m",
                ArgParams = [settings!?.LauncherMemory?.MemoryMaxMb.ToString() ?? ValidationShims.StringEmpty()]
            },
            new MArgument { Arg = "-XX:+AlwaysPreTouch" },
            new MArgument { Arg = "-XX:+DisableExplicitGC" },
            new MArgument { Arg = "-XX:+ParallelRefProcEnabled" },
            new MArgument { Arg = "-XX:+PerfDisableSharedMem" },
            new MArgument { Arg = "-XX:+UnlockExperimentalVMOptions" },
            new MArgument { Arg = "-XX:+UseG1GC" },
            new MArgument { Arg = "-XX:G1HeapRegionSize=8M" },
            new MArgument { Arg = "-XX:G1HeapWastePercent=5" },
            new MArgument { Arg = "-XX:G1MaxNewSizePercent=40" },
            new MArgument { Arg = "-XX:G1MixedGCCountTarget=4" },
            new MArgument { Arg = "-XX:G1MixedGCLiveThresholdPercent=90" },
            new MArgument { Arg = "-XX:G1NewSizePercent=30" },
            new MArgument { Arg = "-XX:G1RSetUpdatingPauseTimePercent=5" },
            new MArgument { Arg = "-XX:G1ReservePercent=20" },
            new MArgument { Arg = "-XX:InitiatingHeapOccupancyPercent=15" },
            new MArgument { Arg = "-XX:MaxGCPauseMillis=200" },
            new MArgument { Arg = "-XX:MaxTenuringThreshold=1" },
            new MArgument { Arg = "-XX:SurvivorRatio=32" },
            new MArgument { Arg = "-Dusing.aikars.flags=https://mcflags.emc.gs" },
            new MArgument { Arg = "-Daikars.new.flags=true" },
            new MArgument
            {
                Arg = "-jar \"{0}\" {1}",
                ArgParams = [PaperPathResolver.JarName(settings!?.LauncherVersion), "nogui"]
            },
        ];
    }
}
