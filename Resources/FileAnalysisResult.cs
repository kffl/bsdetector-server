using System;

namespace BSDetector
{
    public class FileAnalysisResult
    {
        public int LinesAnalyzed { get; set; }
        public Smell[] SmellsDetected { get; set; }
    }
}
