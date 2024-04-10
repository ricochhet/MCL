using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCL.Core.MiniCommon;

#pragma warning disable IDE0079
#pragma warning disable S101
public static class VFS
#pragma warning restore
{
    public static string Cwd { get; set; } = Environment.CurrentDirectory;
    private static readonly object mutex = new();

    /// <summary>
    /// Combine two filepaths.
    /// </summary>
    public static string Combine(string path1, string path2)
    {
        return Path.Combine(path1, path2);
    }

    /// <summary>
    /// Combine three filepaths.
    /// </summary>
    public static string Combine(string path1, string path2, string path3)
    {
        return Path.Combine(path1, path2, path3);
    }

    /// <summary>
    /// Combine four filepaths.
    /// </summary>
    public static string Combine(string path1, string path2, string path3, string path4)
    {
        return Path.Combine(path1, path2, path3, path4);
    }

    /// <summary>
    /// Combine an array of filepaths.
    /// </summary>
    public static string Combine(params string[] paths)
    {
        return Path.Combine(paths);
    }

    /// <summary>
    /// Combines the filepath with the current working directory.
    /// </summary>
    public static string FromCwd(this string filepath)
    {
        return Combine(Cwd, filepath);
    }

    /// <summary>
    /// Get directory name of a filepath
    /// </summary>
    public static string GetDirectoryName(this string filepath)
    {
        string value = Path.GetDirectoryName(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : "";
    }

    /// <summary>
    /// Get file name of a filepath
    /// </summary>
    public static string GetFileName(this string filepath)
    {
        string value = Path.GetFileName(filepath);
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
    /// Get file name without extension of a filepath.
    /// </summary>
    public static string GetFileNameWithoutExtension(this string filepath)
    {
        string value = Path.GetFileNameWithoutExtension(filepath);
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
    /// Copy file from one place to another
    /// </summary>
    public static void CopyFile(string a, string b, bool overwrite = true)
    {
        lock (mutex)
        {
            if (!Exists(b))
            {
                CreateDirectory(b.GetDirectoryName());
            }

            File.Copy(a, b, overwrite);
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
    public static string ReadFile(string filepath, Encoding encoding)
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
                CreateDirectory(filepath.GetDirectoryName());
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
    /// Open a file write stream.
    /// </summary>
    public static FileStream OpenWrite(string filepath)
    {
        return File.OpenWrite(filepath);
    }

    /// <summary>
    /// Open a file read stream.
    /// </summary>
    public static FileStream OpenRead(string filepath)
    {
        return File.OpenRead(filepath);
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
            List<string> paths = [];
            foreach (string file in Directory.EnumerateFiles(filepath, searchPattern, searchOption))
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
    public static void DeleteDirectory(string filepath, bool recursive = false)
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

            di.Delete(recursive);
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
