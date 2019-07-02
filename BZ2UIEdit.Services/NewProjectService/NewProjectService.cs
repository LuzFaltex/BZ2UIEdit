using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BZ2UIEdit.Common;
using BZ2UIEdit.Common.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;

namespace BZ2UIEdit.Services.NewProjectService
{
    public class NewProjectService : INewProjectService
    {
        private readonly ILogger _logger;
        private static readonly string _appDataBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LuzFaltex", "BZ2UIEdit");
        private static readonly string _bziiLocation = Path.Combine(_appDataBase, "Assets", "BZII");
        private static readonly string _bzccLocation = Path.Combine(_appDataBase, "Assets", "BZCC");

        public NewProjectService(Logger logger)
        {
            _logger = logger.ForContext<NewProjectService>();
        }
        
        /// <inheritdoc />
        public async Task<IResult> CreateNewProject(string projectName, string projectLocation, GameType gameType, ProjectType projectType)
        {
            List<string> files = new List<string>();

            _logger.Information("Creating new project with name '{0}' and location '{1}'", projectName, projectLocation);

            // Create directory if it doesn't exist, though it should...
            if (!Directory.Exists(projectLocation))
            {
                try
                {
                    _logger.Debug("Creating project directory");
                    Directory.CreateDirectory(projectLocation);
                }
                catch (Exception ex)
                {
                    return new FailureResult(ex);
                }
            }

            var projectModel = new ProjectModel(projectName, gameType, projectType == ProjectType.EmptyFallback, files);

            var projFileResult = WriteProjectFile(projectLocation, projectModel);
            if (projFileResult is FailureResult)
                return projFileResult;

            if (projectType == ProjectType.VanillaTemplate)
            {
                var cloneResult = await CloneStockUI(projectLocation, gameType);
                if (cloneResult is FailureResult)
                    return cloneResult;
            }

            return new SuccessResult();
        }

        private IResult WriteProjectFile(string projectLocation, ProjectModel projectModel)
        {
            try
            {
                File.WriteAllText(Path.Combine(projectLocation, projectModel.ProjectName, ".bzi"), JsonConvert.SerializeObject(projectModel));
                return new SuccessResult("Project file written to disk.");
            }
            catch (Exception ex)
            {
                return new FailureResult("Failed to create the project file.", ex);
            }
        }

        private async Task<IResult> CloneStockUI(string projectLocation, GameType gameType)
        {
            List<FileInfo> stockUIFiles = new List<FileInfo>();

            // Clone files
            if (gameType == GameType.BZII)
            {
                var dir = new DirectoryInfo(_bziiLocation);
                stockUIFiles.AddRange(dir.GetFiles("*", SearchOption.AllDirectories));

            }
            else if (gameType == GameType.BZCC)
            {
                var dir = new DirectoryInfo(_bzccLocation);
                stockUIFiles.AddRange(dir.GetFiles("*", SearchOption.AllDirectories));
            }
            else
            {
                // BZ98 is not supported at this time
                return new FailureResult(new NotSupportedException("BZ98 and BZ98r are not supported at this time."));
            }

            // Copy the files
            try
            {
                await Task.Run(() =>
                {
                    foreach (var file in stockUIFiles)
                    {
                        // If for whatever reason the file ceases to exist,
                        // skip it.
                        if (!file.Exists)
                        {
                            _logger.Debug("Skipping file {0}: file does not exist.", file.Name);
                            continue;
                        }

                        DirectoryInfo destDir;

                        if (gameType == GameType.BZII)
                            destDir = new DirectoryInfo(file.DirectoryName.Replace(_bziiLocation, projectLocation));
                        else
                            destDir = new DirectoryInfo(file.DirectoryName.Replace(_bzccLocation, projectLocation));

                        // Create the path if it doesn't exist
                        destDir.Create();

                        var newPath = Path.Combine(destDir.FullName, Path.GetFileName(file.FullName));

                        _logger.Debug("Copying file {0} to {1}", file.Name, destDir.FullName);

                        file.CopyTo(newPath, true);
                    }
                });               

                return new SuccessResult("All files copied successfully.");
            }
            catch (Exception ex)
            {
                return new FailureResult("Failed to copy stock UI files.", ex);
            }
        }
    }
}
