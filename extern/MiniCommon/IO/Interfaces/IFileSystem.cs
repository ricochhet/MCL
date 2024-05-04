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
using System.Text;

namespace MiniCommon.IO.Interfaces;

public interface IFileSystem
{
    /// <summary>
    /// Combines two file paths.
    /// </summary>
    public static abstract string Combine(string path1, string path2);

    /// <summary>
    /// Combines three file paths.
    /// </summary>
    public static abstract string Combine(string path1, string path2, string path3);

    /// <summary>
    /// Combines four file paths.
    /// </summary>
    public static abstract string Combine(string path1, string path2, string path3, string path4);

    /// <summary>
    /// Combines an array of file paths.
    /// </summary>
    public static abstract string Combine(params string[] paths);

    /// <summary>
    /// Combines a file path with the current working directory.
    /// </summary>
    public static abstract string FromCwd(string filepath);

    /// <summary>
    /// Combines two file paths with the current working directory.
    /// </summary>
    public static abstract string FromCwd(string path1, string path2);

    /// <summary>
    /// Combines three file paths with the current working directory.
    /// </summary>
    public static abstract string FromCwd(string path1, string path2, string path3);

    /// <summary>
    /// Combines an array of file paths with the current working directory.
    /// </summary>
    public static abstract string FromCwd(params string[] paths);

    /// <summary>
    /// Gets the directory name of a file path.
    /// </summary>
    public static abstract string GetDirectoryName(string filepath);

    /// <summary>
    /// Gets the file name of a file path.
    /// </summary>
    public static abstract string GetFileName(string filepath);

    /// <summary>
    /// Gets the file extension of a file path.
    /// </summary>
    public static abstract string GetFileExtension(string filepath);

    /// <summary>
    /// Gets the file name without extension of a file path.
    /// </summary>
    public static abstract string GetFileNameWithoutExtension(string filepath);

    /// <summary>
    /// Gets the relative path from one path to another.
    /// </summary>
    public static abstract string GetRelativePath(string relativeTo, string path);

    /// <summary>
    /// Gets the relative path to itself.
    /// </summary>
    public static abstract string GetRelativePath(string relativeTo);

    /// <summary>
    /// Moves a file from one place to another.
    /// </summary>
    public static abstract void MoveFile(string a, string b);

    /// <summary>
    /// Copies a file from one place to another.
    /// </summary>
    public static abstract void CopyFile(string a, string b, bool overwrite = true);

    /// <summary>
    /// Copies a directory from one place to another.
    /// </summary>
    public static abstract void CopyDirectory(string a, string b, bool recursive = false);

    /// <summary>
    /// Determines whether the specified file or directory exists.
    /// </summary>
    public static abstract bool Exists(string filepath);

    /// <summary>
    /// Creates a directory.
    /// </summary>
    public static abstract void CreateDirectory(string filepath);

    /// <summary>
    /// Reads the contents of a file as bytes.
    /// </summary>
    public static abstract byte[] ReadFile(string filepath);

    /// <summary>
    /// Reads the contents of a file as a string.
    /// </summary>
    public static abstract string ReadFile(string filepath, Encoding encoding);

    /// <summary>
    /// Reads the contents of a file as a string.
    /// </summary>
    public static abstract string ReadAllText(string filepath);

    /// <summary>
    /// Reads the lines of a file.
    /// </summary>
    public static abstract string[] ReadAllLines(string filepath);

    /// <summary>
    /// Writes data to a file.
    /// </summary>
    public static abstract void WriteFile(string filepath, byte[] data);

    /// <summary>
    /// Writes a string to a file.
    /// </summary>
    public static abstract void WriteFile(string filepath, string data, Encoding? encoding = null);

    /// <summary>
    /// Opens a file for writing.
    /// </summary>
    public static abstract FileStream OpenWrite(string filepath);

    /// <summary>
    /// Opens a file for reading.
    /// </summary>
    public static abstract FileStream OpenRead(string filepath);

    /// <summary>
    /// Gets the directories within a directory.
    /// </summary>
    public static abstract string[] GetDirectories(string filepath);

    /// <summary>
    /// Gets the directories within a directory as DirectoryInfo objects.
    /// </summary>
    public static abstract DirectoryInfo[] GetDirectoryInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    );

    /// <summary>
    /// Gets the files within a directory.
    /// </summary>
    public static abstract string[] GetFiles(string filepath);

    /// <summary>
    /// Gets the files within a directory and its subdirectories.
    /// </summary>
    public static abstract string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    );

    /// <summary>
    /// Gets the files within a directory and its subdirectories as FileInfo objects.
    /// </summary>
    public static abstract FileInfo[] GetFileInfos(string filepath, string searchPattern, SearchOption searchOption);

    /// <summary>
    /// Deletes a directory.
    /// </summary>
    public static abstract void DeleteDirectory(string filepath, bool recursive = false);

    /// <summary>
    /// Deletes a file.
    /// </summary>
    public static abstract void DeleteFile(string filepath);

    /// <summary>
    /// Gets the count of files within a directory and its subdirectories.
    /// </summary>
    public static abstract int GetFilesCount(string filepath);

    /// <summary>
    /// Creates a relative path from one path to another.
    /// </summary>
    public static abstract string MakeRelativePath(string a, string b);
}
