using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BZ2UIEdit.Services.FileService
{
    public interface IFileService
    {
        /// <summary>
        /// Creates a directory in the specified location
        /// </summary>
        /// <param name="location">The location of the directory to create</param>
        DirectoryInfo CreateDirectory(string location);

        /// <summary>
        /// Copies the file identified by the <see cref="FileInfo"/> and copies it to the location specified in <paramref name="destFileName"/>.
        /// </summary>
        /// <param name="file">The file to copy.</param>
        /// <param name="destFileName">The location to place the copied file.</param>
        /// <param name="overwrite">Whether to overwrite any existing files at the location.</param>
        FileInfo CopyFile(FileInfo file, string destFileName, bool overwrite);

        /// <summary>
        /// Writes all text specified by <paramref name="contents"/> to the file specified by <paramref name="path"/>
        /// </summary>
        /// <param name="path">Where to place the written contents.</param>
        /// <param name="contents">What to write.</param>
        void WriteAllText(string path, string contents);
    }
}
