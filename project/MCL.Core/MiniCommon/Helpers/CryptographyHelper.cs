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
