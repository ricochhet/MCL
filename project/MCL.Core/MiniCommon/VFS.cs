using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCL.Core.MiniCommon;

public static class VFS
{
    public static string Cwd { get; set; }
    private static readonly object mutex;

    static VFS()
    {
        Cwd = Environment.CurrentDirectory;
        mutex = new object();
    }

    /// <summary>
    /// Combine two filepaths.
    /// </summary>
    public static string Combine(string path1, string path2)
    {
        return Path.Combine(path1, path2);
    }

    /// <summary>
    /// Combines the filepath with the current working directory.
    /// </summary>
    public static string FromCwd(this string filepath)
    {
        return Combine(Cwd, filepath);
    }

    /// <summary>
    /// Get directory path of a filepath.
    /// </summary>
    public static string GetDirectory(this string filepath)
    {
        string value = Path.GetDirectoryName(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : "";
    }

    /// <summary>
    /// Get file of a filepath
    /// </summary>
    public static string GetFile(this string filepath)
    {
        string value = Path.GetFileName(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : "";
    }

    /// <summary>
    /// Get file name of a filepath
    /// </summary>
    public static string GetFileName(this string filepath)
    {
        string value = Path.GetFileNameWithoutExtension(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : "";
    }

    /// <summary>
    /// Get file extension of a filepath.
    /// </summary>
    public static string GetFileExtension(this string filepath)
    {
        string value = Path.GetExtension(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : "";
    }

    /// <summary>
    /// Move file from one place to another
    /// </summary>
    public static void MoveFile(string a, string b)
    {
        lock (mutex)
        {
            new FileInfo(a).MoveTo(b);
        }
    }

    /// <summary>
    /// Does the filepath exist?
    /// </summary>
    public static bool Exists(string filepath)
    {
        lock (mutex)
        {
            return Directory.Exists(filepath) || File.Exists(filepath);
        }
    }

    /// <summary>
    /// Create directory (recursive).
    /// </summary>
    public static void CreateDirectory(string filepath)
    {
        lock (mutex)
        {
            Directory.CreateDirectory(filepath);
        }
    }

    /// <summary>
    /// Get file content as bytes.
    /// </summary>
    public static byte[] ReadFile(string filepath)
    {
        lock (mutex)
        {
            return File.ReadAllBytes(filepath);
        }
    }

    /// <summary>
    /// Get file content as string.
    /// </summary>
    public static string ReadFile(string filepath, Encoding encoding = null)
    {
        return (encoding ?? Encoding.UTF8).GetString(ReadFile(filepath));
    }

    public static string ReadAllText(string filepath)
    {
        lock (mutex)
        {
            return File.ReadAllText(filepath);
        }
    }

    /// <summary>
    /// Write data to file.
    /// </summary>
    public static void WriteFile(string filepath, byte[] data)
    {
        lock (mutex)
        {
            if (!Exists(filepath))
            {
                CreateDirectory(filepath.GetDirectory());
            }

            File.WriteAllBytes(filepath, data);
        }
    }

    /// <summary>
    /// Write string to file.
    /// </summary>
    public static void WriteFile(string filepath, string data, Encoding encoding = null)
    {
        WriteFile(filepath, (encoding ?? Encoding.UTF8).GetBytes(data));
    }

    /// <summary>
    /// Get directories in directory by full path.
    /// </summary>
    public static string[] GetDirectories(string filepath)
    {
        lock (mutex)
        {
            DirectoryInfo di = new(filepath);
            List<string> paths = [];

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                paths.Add(directory.FullName);
            }

            return [.. paths];
        }
    }

    /// <summary>
    /// Get files in directory by full path.
    /// </summary>
    public static string[] GetFiles(string filepath)
    {
        lock (mutex)
        {
            DirectoryInfo di = new(filepath);
            List<string> paths = [];

            foreach (FileInfo file in di.GetFiles())
            {
                paths.Add(file.FullName);
            }

            return [.. paths];
        }
    }

    /// <summary>
    /// Get files in directory and its sub-directories by full path.
    /// </summary>
    public static string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    )
    {
        lock (mutex)
        {
            IEnumerable<string> enumerable = Directory.EnumerateDirectories(filepath, searchPattern, searchOption);
            List<string> paths = [];
            foreach (string file in enumerable)
            {
                if (includeExtension)
                    paths.Add(file);
                else
                    paths.Add(Path.GetFileNameWithoutExtension(file));
            }

            return [.. paths];
        }
    }

    /// <summary>
    /// Delete directory.
    /// </summary>
    public static void DeleteDirectory(string filepath)
    {
        lock (mutex)
        {
            DirectoryInfo di = new(filepath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.IsReadOnly = false;
                file.Delete();
            }

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                DeleteDirectory(directory.FullName);
            }

            di.Delete();
        }
    }

    /// <summary>
    /// Delete file.
    /// </summary>
    public static void DeleteFile(string filepath)
    {
        lock (mutex)
        {
            FileInfo file = new(filepath) { IsReadOnly = false };
            file.Delete();
        }
    }

    /// <summary>
    /// Get files count inside directory recusively
    /// </summary>
    public static int GetFilesCount(string filepath)
    {
        lock (mutex)
        {
            DirectoryInfo di = new(filepath);
            int count = 0;

            foreach (FileInfo file in di.GetFiles())
            {
                ++count;
            }

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                count += GetFilesCount(directory.FullName);
            }

            return count;
        }
    }
}
