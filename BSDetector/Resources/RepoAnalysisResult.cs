using System.Collections.Generic;
using BSDetector;

namespace BSDetector.Resources
{
    /// <summary>
    /// Represents multiple file analysis results
    /// </summary>
    public class RepoAnalysisResult
    {
        public List<FileAnalysisResult> Files { get; set; }
    }
}