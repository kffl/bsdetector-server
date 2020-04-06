using System;

namespace BSDetector
{
    public class FileAnalysisResult
    {
        public class Occurance
        {
            public string Snippet {get; set;}
            public int LineStart {get; set;}
            public int ColStart {get; set;}
            public int LineEnd {get; set;}
            public int ColEnd {get; set;}
        }
        public class Smell
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Occurance[] Occurances { get; set; }
            
        }
        public int LinesAnalyzed { get; set; }
        public Smell[] SmellsDetected { get; set; }
    }
}
