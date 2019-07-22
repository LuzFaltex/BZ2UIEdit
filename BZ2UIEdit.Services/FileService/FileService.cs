using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BZ2UIEdit.Services.FileService
{
    public class FileService : IFileService
    {
        /// <inheritdoc />
        public FileInfo CopyFile(FileInfo file, string destFileName, bool overwrite)
            => file.CopyTo(destFileName, overwrite);

        /// <inheritdoc />
        public DirectoryInfo CreateDirectory(string location)
            => Directory.CreateDirectory(location);

        /// <inheritdoc />
        public void WriteAllText(string path, string contents)
            => File.WriteAllText(path, contents);
    }
}
