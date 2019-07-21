using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BZ2UIEdit.Common;
using BZ2UIEdit.Common.Models;
using BZ2UIEdit.Services.FileService;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Serilog;

namespace BZ2UIEdit.Services.NewProjectService
{
    public class NewProjectService : INewProjectService
    {
        private readonly ILogger _logger;
        private static readonly string _appDataBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LuzFaltex", "BZ2UIEdit");
        private static readonly string _bziiLocation = Path.Combine(_appDataBase, "Assets", "BZII");
        private static readonly string _bzccLocation = Path.Combine(_appDataBase, "Assets", "BZCC");

        private readonly IFileService _fileService;

        public NewProjectService(ILogger logger, IFileService fileService)
        {
            _logger = logger.ForContext<NewProjectService>();
            _fileService = fileService;
        }
        
        /// <inheritdoc />
        public async Task<IResult> CreateNewProject(string projectName, string projectLocation, GameType gameType, bool cloneStock, bool fallback, ProgressDialogController controller)
        {
            List<string> files = new List<string>();

            _logger.Information("Creating new project with name '{0}' and location '{1}'", projectName, projectLocation);

            // Create directory if it doesn't exist, though it should...
            if (!Directory.Exists(projectLocation))
            {
                try
                {
                    _logger.Debug("Creating project directory");
                    _fileService.CreateDirectory(projectLocation);
                }
                catch (Exception ex)
                {
                    return new FailureResult(ex);
                }
            }

            var projectModel = new ProjectModel(projectName, gameType, fallback, files);

            var projFileResult = WriteProjectFile(projectLocation, projectModel, controller);
            if (projFileResult is FailureResult)
                return projFileResult;

            if (cloneStock)
            {
                var cloneResult = await CloneStockUI(projectLocation, gameType, controller);
                if (cloneResult is FailureResult)
                    return cloneResult;
            }

            return new SuccessResult();
        }

        private IResult WriteProjectFile(string projectLocation, ProjectModel projectModel, ProgressDialogController controller)
        {
            controller.SetMessage("Writing project file...");
            try
            {
                _fileService.WriteAllText(Path.Combine(projectLocation, projectModel.ProjectName + ".bzi"), JsonConvert.SerializeObject(projectModel, Formatting.Indented));
                controller.SetMessage("Writing project file... Done!");
                return new SuccessResult("Project file written to disk.");
            }
            catch (Exception ex)
            {
                return new FailureResult("Failed to create the project file.", ex);
            }
        }

        private async Task<IResult> CloneStockUI(string projectLocation, GameType gameType, ProgressDialogController controller)
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
                    // foreach (var file in stockUIFiles)
                    for (int i = 0; i < stockUIFiles.Count; i++)
                    {
                        controller.SetProgress((double)i / stockUIFiles.Count);

                        // If for whatever reason the file ceases to exist,
                        // skip it.
                        var file = stockUIFiles[i];
                        if (!file.Exists)
                        {
                            _logger.Debug("Skipping file '{0}': file does not exist.", file.Name);
                            continue;
                        }

                        DirectoryInfo destDir;

                        if (gameType == GameType.BZII)
                            destDir = new DirectoryInfo(file.DirectoryName.Replace(_bziiLocation, projectLocation));
                        else
                            destDir = new DirectoryInfo(file.DirectoryName.Replace(_bzccLocation, projectLocation));

                        // Create the path if it doesn't exist
                        // destDir.Create();
                        controller.SetMessage($"Creating directory {destDir.FullName}");
                        _fileService.CreateDirectory(destDir.FullName);

                        var newPath = Path.Combine(destDir.FullName, Path.GetFileName(file.FullName));

                        controller.SetMessage($"Copying file '{file.Name}' to '{destDir.FullName}'");
                        _logger.Debug("Copying file '{0}' to '{1}'", file.Name, destDir.FullName);

                        _fileService.CopyFile(file, newPath, true);
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
