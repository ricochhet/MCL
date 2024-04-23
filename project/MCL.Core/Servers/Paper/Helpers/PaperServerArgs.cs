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

using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperServerArgs
{
    /// <summary>
    /// The default JvmArguments to launch the process with.
    /// </summary>
    public static JvmArguments DefaultJvmArguments(Settings settings)
    {
        if (ObjectValidator<Settings>.IsNull(settings))
            return null;

        JvmArguments jvmArguments = new();
        jvmArguments.Add("-Xms{0}m", [settings.LauncherMemory.MemoryMinMb.ToString()]);
        jvmArguments.Add("-Xmx{0}m", [settings.LauncherMemory.MemoryMaxMb.ToString()]);
        jvmArguments.Add("-XX:+AlwaysPreTouch");
        jvmArguments.Add("-XX:+DisableExplicitGC");
        jvmArguments.Add("-XX:+ParallelRefProcEnabled");
        jvmArguments.Add("-XX:+PerfDisableSharedMem");
        jvmArguments.Add("-XX:+UnlockExperimentalVMOptions");
        jvmArguments.Add("-XX:+UseG1GC");
        jvmArguments.Add("-XX:G1HeapRegionSize=8M");
        jvmArguments.Add("-XX:G1HeapWastePercent=5");
        jvmArguments.Add("-XX:G1MaxNewSizePercent=40");
        jvmArguments.Add("-XX:G1MixedGCCountTarget=4");
        jvmArguments.Add("-XX:G1MixedGCLiveThresholdPercent=90");
        jvmArguments.Add("-XX:G1NewSizePercent=30");
        jvmArguments.Add("-XX:G1RSetUpdatingPauseTimePercent=5");
        jvmArguments.Add("-XX:G1ReservePercent=20");
        jvmArguments.Add("-XX:InitiatingHeapOccupancyPercent=15");
        jvmArguments.Add("-XX:MaxGCPauseMillis=200");
        jvmArguments.Add("-XX:MaxTenuringThreshold=1");
        jvmArguments.Add("-XX:SurvivorRatio=32");
        jvmArguments.Add("-Dusing.aikars.flags=https://mcflags.emc.gs");
        jvmArguments.Add("-Daikars.new.flags=true");
        jvmArguments.Add(
            "-jar {0} {1}",
            [PaperPathResolver.JarPath(settings.LauncherPath, settings.LauncherVersion), "nogui"]
        );

        return jvmArguments;
    }
}
