using BZ2UIEdit.Common;
using BZ2UIEdit.Common.Models;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using System.Threading.Tasks;

namespace BZ2UIEdit.Services.NewProjectService
{
    public interface INewProjectService
    {
        /// <summary>
        /// Creates a new project in the specified location and pre-populates any appropriate files based on the selected project type.
        /// </summary>
        /// <param name="projectName">The name of the project</param>
        /// <param name="projectFile">A <see cref="FileInfo"/> representing the file information as selected by the user.</param>
        /// <param name="gameType">The <see cref="GameType"/> determining file compatibility</param>
        /// <param name="cloneStock">Whether or not to clone the default project files.</param>
        /// <param name="fallback">Whether or not to fall back to default files.</param>
        /// <param name="controller">The <see cref="ProgressDialogController"/> showing the current progress.</param>
        /// <returns>A new <see cref="IResult"/> determining success or failure of the operation.</returns>
        Task<IResult> CreateNewProject(string projectName, FileInfo projectFile, GameType gameType, bool cloneStock, bool fallback, ProgressDialogController controller);
    }
}
