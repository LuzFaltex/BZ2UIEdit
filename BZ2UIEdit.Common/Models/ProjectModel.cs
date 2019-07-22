using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace BZ2UIEdit.Common.Models
{
    [JsonObject]
    public class ProjectModel
    {
        /// <summary>
        /// Gets or sets the Project Name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the Game (BZII or BZCC)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GameType GameType { get; set; }

        /// <summary>
        /// Gets or sets whether missing files fall back to default
        /// </summary>
        public bool FallbackToDefaults { get; set; }

        /// <summary>
        /// Gets a list of files tracked by this project as a relative path to the file.
        /// </summary>
        public List<string> Files { get; }

        [JsonConstructor]
        public ProjectModel(string projectName, GameType gameType, bool fallbackToDefaults, IEnumerable<string> files)
        {
            ProjectName = projectName;
            GameType = gameType;
            FallbackToDefaults = fallbackToDefaults;
            Files = new List<string>(files);
        }

        public ProjectModel(string projectName, GameType gameType, bool fallbackToDefaults) : this(projectName, gameType, fallbackToDefaults, new string[0])
        {

        }
    }
}
