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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MCL.Core.MiniCommon.Helpers;

public static class CryptographyHelper
{
    /// <summary>
    /// Create a SHA1 hash from a filestream, and return as string.
    /// </summary>
    public static string CreateSHA1(string fileName, bool formatting)
    {
        if (!VFS.Exists(fileName))
            return string.Empty;

        using FileStream stream = VFS.OpenRead(fileName);
        byte[] hash = SHA1.HashData(stream);

        if (formatting)
            return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);

        return BitConverter.ToString(hash);
    }

    /// <summary>
    /// Create a SHA1 hash from a string, and return as string.
    /// </summary>
    public static string CreateSHA1(string value, Encoding enc)
    {
        StringBuilder stringBuilder = new();
        byte[] hash = SHA1.HashData(enc.GetBytes(value));
        foreach (byte b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Create a SHA256 hash from a filestream, and return as string.
    /// </summary>
    public static string CreateSHA256(string fileName, bool formatting)
    {
        if (!VFS.Exists(fileName))
            return string.Empty;

        using FileStream stream = VFS.OpenRead(fileName);
        byte[] hash = SHA256.HashData(stream);

        if (formatting)
            return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);

        return BitConverter.ToString(hash);
    }

    /// <summary>
    /// Create a SHA256 hash from a string, and return as string.
    /// </summary>
    public static string CreateSHA256(string value, Encoding enc)
    {
        StringBuilder stringBuilder = new();
        byte[] hash = SHA256.HashData(enc.GetBytes(value));
        foreach (byte b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Create an UUID from an MD5 hash, and return as string.
    /// </summary>
    public static string CreateUUID(string value)
    {
        byte[] digestedHash = MD5.HashData(Encoding.UTF8.GetBytes(value));
        digestedHash[6] = (byte)((digestedHash[6] & 0x0f) | 0x30);
        digestedHash[8] = (byte)((digestedHash[8] & 0x3f) | 0x80);
        string encoded = BitConverter.ToString(digestedHash).Replace("-", string.Empty).ToLower();
        return encoded;
    }
}
