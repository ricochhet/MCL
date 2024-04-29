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
using System.Text;
using MCL.Core.MiniCommon.IO.Interfaces;

namespace MCL.Core.MiniCommon.IO.Abstractions;

public abstract class BaseFileSystem : IBaseFileSystem
{
    public virtual string Cwd { get; set; } = Environment.CurrentDirectory;
    private static readonly object _mutex = new();

    /// <inheritdoc />
    public virtual string Combine(string path1, string path2)
    {
        return Path.Combine(path1, path2);
    }

    /// <inheritdoc />
    public virtual string Combine(string path1, string path2, string path3)
    {
        return Path.Combine(path1, path2, path3);
    }

    /// <inheritdoc />
    public virtual string Combine(string path1, string path2, string path3, string path4)
    {
        return Path.Combine(path1, path2, path3, path4);
    }

    /// <inheritdoc />
    public virtual string Combine(params string[] paths)
    {
        return Path.Combine(paths);
    }

    /// <inheritdoc />
    public virtual string FromCwd(string filepath)
    {
        return Combine(Cwd, filepath);
    }

#pragma warning disable IDE0079
#pragma warning disable S2234
    /// <inheritdoc />
    public virtual string FromCwd(string path1, string path2)
    {
        return Combine(Cwd, path1, path2);
    }

    /// <inheritdoc />
    public virtual string FromCwd(string path1, string path2, string path3)
    {
        return Combine(Cwd, path1, path2, path3);
    }
#pragma warning restore IDE0079, S2234

    /// <inheritdoc />
    public virtual string FromCwd(params string[] paths)
    {
        return Combine(paths.Prepend(Cwd).ToArray());
    }

    /// <inheritdoc />
    public virtual string GetDirectoryName(string filepath)
    {
        string value = Path.GetDirectoryName(filepath) ?? string.Empty;
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual string GetFileName(string filepath)
    {
        string value = Path.GetFileName(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual string GetFileExtension(string filepath)
    {
        string value = Path.GetExtension(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual string GetFileNameWithoutExtension(string filepath)
    {
        string value = Path.GetFileNameWithoutExtension(filepath);
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual string GetRelativePath(string relativeTo, string path)
    {
        string value = Path.GetRelativePath(relativeTo, path);
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual string GetRelativePath(string relativeTo)
    {
        string value = Path.GetRelativePath(relativeTo, relativeTo);
        return (!string.IsNullOrWhiteSpace(value)) ? value : string.Empty;
    }

    /// <inheritdoc />
    public virtual void MoveFile(string a, string b)
    {
        lock (_mutex)
        {
            new FileInfo(a).MoveTo(b);
        }
    }

    /// <inheritdoc />
    public virtual void CopyFile(string a, string b, bool overwrite = true)
    {
        lock (_mutex)
        {
            if (!Exists(b))
            {
                CreateDirectory(GetDirectoryName(b));
            }

            File.Copy(a, b, overwrite);
        }
    }

    /// <inheritdoc />
    public virtual void CopyDirectory(string a, string b, bool recursive = false)
    {
        lock (_mutex)
        {
            DirectoryInfo directory = new(a);

            if (!directory.Exists)
                return;

            DirectoryInfo[] directories = directory.GetDirectories();
            Directory.CreateDirectory(b);
            foreach (FileInfo file in directory.GetFiles())
            {
                string destination = Path.Combine(b, file.Name);
                file.CopyTo(destination);
            }

            if (!recursive)
                return;

            foreach (DirectoryInfo subDirectory in directories)
            {
                string destination = Path.Combine(b, subDirectory.Name);
                CopyDirectory(subDirectory.FullName, destination, true);
            }
        }
    }

    /// <inheritdoc />
    public virtual bool Exists(string filepath)
    {
        lock (_mutex)
        {
            return Directory.Exists(filepath) || File.Exists(filepath);
        }
    }

    /// <inheritdoc />
    public virtual void CreateDirectory(string filepath)
    {
        lock (_mutex)
        {
            Directory.CreateDirectory(filepath);
        }
    }

    /// <inheritdoc />
    public virtual byte[] ReadFile(string filepath)
    {
        lock (_mutex)
        {
            return File.ReadAllBytes(filepath);
        }
    }

    /// <inheritdoc />
    public virtual string ReadFile(string filepath, Encoding encoding)
    {
        return (encoding ?? Encoding.UTF8).GetString(ReadFile(filepath));
    }

    /// <inheritdoc />
    public virtual string ReadAllText(string filepath)
    {
        lock (_mutex)
        {
            return File.ReadAllText(filepath);
        }
    }

    /// <inheritdoc />
    public virtual string[] ReadAllLines(string filepath)
    {
        lock (_mutex)
        {
            return File.ReadAllLines(filepath);
        }
    }

    /// <inheritdoc />
    public virtual void WriteFile(string filepath, byte[] data)
    {
        lock (_mutex)
        {
            if (!Exists(filepath))
            {
                CreateDirectory(GetDirectoryName(filepath));
            }

            File.WriteAllBytes(filepath, data);
        }
    }

    /// <inheritdoc />
    public virtual void WriteFile(string filepath, string data, Encoding? encoding = null)
    {
        WriteFile(filepath, (encoding ?? Encoding.UTF8).GetBytes(data));
    }

    /// <inheritdoc />
    public virtual FileStream OpenWrite(string filepath)
    {
        if (!Exists(filepath))
        {
            WriteFile(filepath, string.Empty);
        }

        return File.OpenWrite(filepath);
    }

    /// <inheritdoc />
    public virtual FileStream OpenRead(string filepath)
    {
        return File.OpenRead(filepath);
    }

    /// <inheritdoc />
    public virtual string[] GetDirectories(string filepath)
    {
        lock (_mutex)
        {
            if (!Exists(filepath))
                return Enumerable.Empty<string>().ToArray();

            DirectoryInfo di = new(filepath);
            List<string> paths = [];

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                paths.Add(directory.FullName);
            }

            return [.. paths];
        }
    }

    /// <inheritdoc />
    public virtual DirectoryInfo[] GetDirectoryInfos(string filepath, string searchPattern, SearchOption searchOption)
    {
        return new DirectoryInfo(filepath).GetDirectories(searchPattern, searchOption);
    }

    /// <inheritdoc />
    public virtual string[] GetFiles(string filepath)
    {
        lock (_mutex)
        {
            if (!Exists(filepath))
                return Enumerable.Empty<string>().ToArray();

            DirectoryInfo di = new(filepath);
            List<string> paths = [];

            foreach (FileInfo file in di.GetFiles())
            {
                paths.Add(file.FullName);
            }

            return [.. paths];
        }
    }

    /// <inheritdoc />
    public virtual string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    )
    {
        lock (_mutex)
        {
            if (!Exists(filepath))
                return Enumerable.Empty<string>().ToArray();

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

    /// <inheritdoc />
    public virtual FileInfo[] GetFileInfos(string filepath, string searchPattern, SearchOption searchOption)
    {
        return new DirectoryInfo(filepath).GetFiles(searchPattern, searchOption);
    }

    /// <inheritdoc />
    public virtual void DeleteDirectory(string filepath, bool recursive = false)
    {
        lock (_mutex)
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

    /// <inheritdoc />
    public virtual void DeleteFile(string filepath)
    {
        lock (_mutex)
        {
            FileInfo file = new(filepath) { IsReadOnly = false };
            file.Delete();
        }
    }

    /// <inheritdoc />
    public virtual int GetFilesCount(string filepath)
    {
        lock (_mutex)
        {
            DirectoryInfo di = new(filepath);
            int count = 0;

            foreach (FileInfo file in di.GetFiles())
                ++count;

            foreach (DirectoryInfo directory in di.GetDirectories())
                count += GetFilesCount(directory.FullName);

            return count;
        }
    }

    /// <inheritdoc />
    public virtual string MakeRelativePath(string a, string b)
    {
        if (b.StartsWith(a))
            return b[(a.Length + 1)..];

        string[] baseDirs = a.Split(':', '\\', '/');
        string[] fileDirs = b.Split(':', '\\', '/');

        if (baseDirs.Length == 0 || fileDirs.Length == 0 || baseDirs[0] != fileDirs[0])
            return b;

        int offset = 1;
        for (; offset < baseDirs.Length; offset++)
        {
            if (baseDirs[offset] != fileDirs[offset])
                break;
        }

        StringBuilder resultBuilder = new();
        for (int i = 0; i < (baseDirs.Length - offset); i++)
            resultBuilder.Append("..\\");

        for (int i = offset; i < fileDirs.Length - 1; i++)
            resultBuilder.Append(fileDirs[i]).Append('\\');

        resultBuilder.Append(fileDirs[^1]);
        return resultBuilder.ToString();
    }
}
