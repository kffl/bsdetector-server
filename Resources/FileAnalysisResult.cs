using System;
using BSDetector.Resources;

namespace BSDetector.Resources
{
    /// <summary>
    /// Represents results of file analysis performed on a single file
    /// </summary>
    public class FileAnalysisResult
    {
        public string FileName { get; set; }
        public int LinesAnalyzed { get; set; }
        public Smell[] SmellsDetected { get; set; }
        public ErrorResponse error { get; set; }
    }
}
