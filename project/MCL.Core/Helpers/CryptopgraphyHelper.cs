using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCL.Core.Helpers;

public static class CryptographyHelper
{
    public static string Sha1(string fileName)
    {
        if (!File.Exists(fileName))
            return string.Empty;

        using FileStream stream = File.OpenRead(fileName);
        byte[] hash = SHA1.HashData(stream);

        return BitConverter.ToString(hash);
    }

    public static string Sha1(string value, Encoding enc)
    {
        StringBuilder stringBuilder = new();
        byte[] hash = SHA1.HashData(enc.GetBytes(value));
        foreach (byte b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public static string UUID(string value)
    {
        byte[] digestedHash = MD5.HashData(Encoding.UTF8.GetBytes(value));
        digestedHash[6] = (byte)((digestedHash[6] & 0x0f) | 0x30);
        digestedHash[8] = (byte)((digestedHash[8] & 0x3f) | 0x80);
        string encoded = BitConverter.ToString(digestedHash).Replace("-", "").ToLower();
        return encoded;
    }
}
