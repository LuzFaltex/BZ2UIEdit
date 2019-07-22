using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BZ2UIEdit.Services.FileService
{
    public interface IFileService
    {
        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// </summary>
        /// <param name="location">The location of the directory to create</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        DirectoryInfo CreateDirectory(string location);

        /// <summary>
        /// Copies the file identified by the <see cref="FileInfo"/> and copies it to the location specified in <paramref name="destFileName"/>.
        /// </summary>
        /// <param name="file">The file to copy.</param>
        /// <param name="destFileName">The location to place the copied file.</param>
        /// <param name="overwrite">Whether to overwrite any existing files at the location.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        FileInfo CopyFile(FileInfo file, string destFileName, bool overwrite);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target already exists, it is overwritten.
        /// </summary>
        /// <param name="path">Where to place the written contents.</param>
        /// <param name="contents">What to write.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        void WriteAllText(string path, string contents);
    }
}
