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
using System.Text.RegularExpressions;
using MiniCommon.Cryptography.Helpers;

namespace MCL.Core.Launcher.Models;

public partial class LauncherUsername()
{
    public string Username { get; set; } = "Player";
    public string UserType { get; set; } = "legacy";
    public string AccessToken { get; set; } = "1337535510N";

    private const string DefaultUsername = "Player";

    public string ValidateUsername(int length = 16)
    {
        if (Username.Length > length)
            return Username[..length];
        Regex re = UsernameRegex();
        if (re.IsMatch(Username))
            return Username;
        return DefaultUsername;
    }

    public string UUID() => CryptographyHelper.CreateUUID(ValidateUsername());

    public string OfflineUUID() => CryptographyHelper.CreateUUID($"OfflinePlayer:{ValidateUsername()}");

    [GeneratedRegex(@"^[a-zA-Z0-9_]{3,16}$")]
    private static partial Regex UsernameRegex();
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(LauncherUsername))]
internal partial class LauncherUsernameContext : JsonSerializerContext;
