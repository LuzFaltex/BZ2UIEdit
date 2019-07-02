using BZ2UIEdit.Common;
using System.Threading.Tasks;

namespace BZ2UIEdit.Services.NewProjectService
{
    public interface INewProjectService
    {
        /// <summary>
        /// Creates a new project in the specified location and pre-populates any appropriate files based on the selected project type.
        /// </summary>
        /// <param name="projectName">The name of the project</param>
        /// <param name="projectLocation">The location in which the project is held</param>
        /// <param name="gameType">The <see cref="GameType"/> determining file compatibility</param>
        /// <param name="projectType">The <see cref="ProjectType"/> determining how to build the project</param>
        /// <returns>A new <see cref="IResult"/> determining success or failure of the operation.</returns>
        Task<IResult> CreateNewProject(string projectName, string projectLocation, GameType gameType, ProjectType projectType);
    }
}
