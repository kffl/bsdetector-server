using System;
using BSDetector.Resources;

namespace BSDetector
{
    public class FileAnalysisResult
    {
        public string FileName { get; set; }
        public int LinesAnalyzed { get; set; }
        public Smell[] SmellsDetected { get; set; }
        public ErrorResponse error { get; set; }
    }
}
