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

using System.IO;
using System.Linq;
using System.Text;
using MiniCommon.IO.Abstractions;
using MiniCommon.IO.Interfaces;

namespace MiniCommon.IO;

#pragma warning disable S101
public class VFS : IFileSystem
#pragma warning restore S101
{
    public static ValidatedFileSystem FileSystem { get; } = new();

    /// <inheritdoc />
    public static string Combine(string path1, string path2)
    {
        return FileSystem.Combine(path1, path2);
    }

    /// <inheritdoc />
    public static string Combine(string path1, string path2, string path3)
    {
        return FileSystem.Combine(path1, path2, path3);
    }

    /// <inheritdoc />
    public static string Combine(string path1, string path2, string path3, string path4)
    {
        return FileSystem.Combine(path1, path2, path3, path4);
    }

    /// <inheritdoc />
    public static string Combine(params string[] paths)
    {
        return FileSystem.Combine(paths);
    }

    /// <inheritdoc />
    public static string FromCwd(string filepath)
    {
        return FileSystem.Combine(FileSystem.Cwd, filepath);
    }

#pragma warning disable S2234
    /// <inheritdoc />
    public static string FromCwd(string path1, string path2)
    {
        return FileSystem.Combine(FileSystem.Cwd, path1, path2);
    }

    /// <inheritdoc />
    public static string FromCwd(string path1, string path2, string path3)
    {
        return FileSystem.Combine(FileSystem.Cwd, path1, path2, path3);
    }
#pragma warning restore S2234

    /// <inheritdoc />
    public static string FromCwd(params string[] paths)
    {
        return FileSystem.Combine(paths.Prepend(FileSystem.Cwd).ToArray());
    }

    /// <inheritdoc />
    public static string GetDirectoryName(string filepath)
    {
        return FileSystem.GetDirectoryName(filepath);
    }

    /// <inheritdoc />
    public static string GetFileName(string filepath)
    {
        return FileSystem.GetFileName(filepath);
    }

    /// <inheritdoc />
    public static string GetFileExtension(string filepath)
    {
        return FileSystem.GetFileExtension(filepath);
    }

    /// <inheritdoc />
    public static string GetFileNameWithoutExtension(string filepath)
    {
        return FileSystem.GetFileNameWithoutExtension(filepath);
    }

    /// <inheritdoc />
    public static string GetRelativePath(string relativeTo, string path)
    {
        return FileSystem.GetRelativePath(relativeTo, path);
    }

    /// <inheritdoc />
    public static string GetRelativePath(string relativeTo)
    {
        return FileSystem.GetRelativePath(relativeTo);
    }

    /// <inheritdoc />
    public static void MoveFile(string a, string b)
    {
        FileSystem.MoveFile(a, b);
    }

    /// <inheritdoc />
    public static void CopyFile(string a, string b, bool overwrite = true)
    {
        FileSystem.CopyFile(a, b, overwrite);
    }

    /// <inheritdoc />
    public static void CopyDirectory(string a, string b, bool recursive = false)
    {
        FileSystem.CopyDirectory(a, b, recursive);
    }

    /// <inheritdoc />
    public static bool Exists(string filepath)
    {
        return FileSystem.Exists(filepath);
    }

    /// <inheritdoc />
    public static void CreateDirectory(string filepath)
    {
        FileSystem.CreateDirectory(filepath);
    }

    /// <inheritdoc />
    public static byte[] ReadFile(string filepath)
    {
        return FileSystem.ReadFile(filepath);
    }

    /// <inheritdoc />
    public static string ReadFile(string filepath, Encoding encoding)
    {
        return FileSystem.ReadFile(filepath, encoding);
    }

    /// <inheritdoc />
    public static string ReadAllText(string filepath)
    {
        return FileSystem.ReadAllText(filepath);
    }

    /// <inheritdoc />
    public static string[] ReadAllLines(string filepath)
    {
        return FileSystem.ReadAllLines(filepath);
    }

    /// <inheritdoc />
    public static void WriteFile(string filepath, byte[] data)
    {
        FileSystem.WriteFile(filepath, data);
    }

    /// <inheritdoc />
    public static void WriteFile(string filepath, string data, Encoding? encoding = null)
    {
        FileSystem.WriteFile(filepath, data, encoding);
    }

    /// <inheritdoc />
    public static FileStream OpenWrite(string filepath)
    {
        return FileSystem.OpenWrite(filepath);
    }

    /// <inheritdoc />
    public static FileStream OpenRead(string filepath)
    {
        return FileSystem.OpenRead(filepath);
    }

    /// <inheritdoc />
    public static string[] GetDirectories(string filepath)
    {
        return FileSystem.GetDirectories(filepath);
    }

    /// <inheritdoc />
    public static DirectoryInfo[] GetDirectoryInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    )
    {
        return FileSystem.GetDirectoryInfos(filepath, searchPattern, searchOption);
    }

    /// <inheritdoc />
    public static string[] GetFiles(string filepath)
    {
        return FileSystem.GetFiles(filepath);
    }

    /// <inheritdoc />
    public static string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    )
    {
        return FileSystem.GetFiles(filepath, searchPattern, searchOption, includeExtension);
    }

    /// <inheritdoc />
    public static FileInfo[] GetFileInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    )
    {
        return FileSystem.GetFileInfos(filepath, searchPattern, searchOption);
    }

    /// <inheritdoc />
    public static void DeleteDirectory(string filepath, bool recursive = false)
    {
        FileSystem.DeleteDirectory(filepath, recursive);
    }

    /// <inheritdoc />
    public static void DeleteFile(string filepath)
    {
        FileSystem.DeleteFile(filepath);
    }

    /// <inheritdoc />
    public static int GetFilesCount(string filepath)
    {
        return FileSystem.GetFilesCount(filepath);
    }

    /// <inheritdoc />
    public static string MakeRelativePath(string a, string b)
    {
        return FileSystem.MakeRelativePath(a, b);
    }
}
