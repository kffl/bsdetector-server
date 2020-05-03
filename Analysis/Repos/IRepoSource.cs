using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSDetector.Analysis.Repos
{
    /// <summary>
    /// Represents a repository of files
    /// </summary>
    public interface IRepoSource
    {
        /// <summary>
        /// Gets all files in a repository
        /// </summary>
        /// <returns>Collection of files</returns>
        public IEnumerable<IRepoFile> GetFiles();
    }
}