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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCL.Core.MiniCommon.Cryptography.Enums;
using MCL.Core.MiniCommon.Cryptography.Helpers;
using MCL.Core.MiniCommon.Cryptography.Models;
using MCL.Core.MiniCommon.Extensions;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Resolvers;

namespace MCL.Core.MiniCommon.Cryptography;

public static class HashVerifier
{
    /// <summary>
    /// Write a hash verification file to the specified file path.
    /// </summary>
    public static void Write(HashType hashType, string toVerify, string filePath)
    {
        List<string> fileData = [];
        string[] files = VFS.GetFiles(toVerify, "*", SearchOption.AllDirectories);
        fileData.Add(hashType.ToString());
        foreach (string file in files)
            fileData.Add($"{file}={ComputeHash(hashType, file)}");

        if (!VFS.Exists(VFS.GetDirectoryName(filePath)))
            VFS.CreateDirectory(VFS.GetDirectoryName(filePath));
        VFS.WriteFile(filePath, string.Join("\n", fileData));
    }

    /// <summary>
    /// Verify hashes from a hash verification file.
    /// </summary>
    public static List<HashVerifiedFile> Verify(string filePath)
    {
        List<HashVerifiedFile> hashVerifiedFiles = [];
        if (!VFS.Exists(filePath))
            return hashVerifiedFiles;

        string[] lines = VFS.ReadAllLines(filePath);
        HashType hashType = EnumResolver.Parse(lines.FirstOrDefault() ?? "md5", HashType.MD5);
        Dictionary<string, string> hashes = [];
        foreach (string line in lines.Skip(1))
            hashes = hashes.Concat(line.ParseKeyValuePairs()).ToDictionary();
        hashes = hashes.GroupBy(a => a).Select(a => a.Last()).ToDictionary();

        foreach ((string key, string value) in hashes)
        {
            if (!VFS.Exists(key))
            {
                hashVerifiedFiles.Add(
                    new()
                    {
                        FileName = key,
                        CurrentHash = string.Empty,
                        OriginalHash = value,
                        Verified = false
                    }
                );
            }

            string currentHash = ComputeHash(hashType, key);
            if (value != currentHash)
            {
                hashVerifiedFiles.Add(
                    new()
                    {
                        FileName = key,
                        CurrentHash = currentHash,
                        OriginalHash = value,
                        Verified = false
                    }
                );
            }

            if (value == currentHash)
            {
                hashVerifiedFiles.Add(
                    new()
                    {
                        FileName = key,
                        CurrentHash = currentHash,
                        OriginalHash = value,
                        Verified = true
                    }
                );
            }
        }

        return hashVerifiedFiles;
    }

    /// <summary>
    /// Compute a file hash based on the specified HashType.
    /// </summary>
    private static string ComputeHash(HashType hashType, string filePath)
    {
        return hashType switch
        {
            HashType.MD5 => CryptographyHelper.CreateMD5(filePath, true),
            HashType.SHA1 => CryptographyHelper.CreateSHA1(filePath, true),
            HashType.SHA256 => CryptographyHelper.CreateSHA256(filePath, true),
            _ => throw new ArgumentOutOfRangeException(nameof(hashType), "Unsupported hash type."),
        };
    }
}
