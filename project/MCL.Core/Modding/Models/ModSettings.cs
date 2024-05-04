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

using System.Collections.Generic;
using System.Text.Json.Serialization;
using MiniCommon.Validation.Operators;

namespace MCL.Core.Modding.Models;

public class ModSettings
{
    public List<string> CopyOnlyTypes { get; set; } = [".jar"];
    public List<string> UnzipAndCopyTypes { get; set; } = [".zip", ".rar", ".7z"];
    public List<string> ModStores { get; set; } = [];
    public List<string> DeployPaths { get; set; } = [];

    public ModSettings() { }

    public bool IsStoreSaved(string? modStoreName) => ModStores.Contains(modStoreName ?? StringOperator.Empty());

    public bool IsDeployPathSaved(string deployPathName) => DeployPaths.Contains(deployPathName);
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ModSettings))]
internal partial class ModSettingsContext : JsonSerializerContext;
