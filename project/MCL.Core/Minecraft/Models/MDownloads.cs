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

using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MDownloads(MDownload client, MDownload clientMappings, MDownload server, MDownload serverMappings)
{
    [JsonPropertyName("client")]
    public MDownload Client { get; set; } = client;

    [JsonPropertyName("client_mappings")]
    public MDownload ClientMappings { get; set; } = clientMappings;

    [JsonPropertyName("server")]
    public MDownload Server { get; set; } = server;

    [JsonPropertyName("server_mappings")]
    public MDownload ServerMappings { get; set; } = serverMappings;
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MDownloads))]
internal partial class MDownloadsContext : JsonSerializerContext;
