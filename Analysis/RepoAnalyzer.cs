using System.Collections.Generic;
using BSDetector.Analysis.Repos;
using BSDetector.Resources;

namespace BSDetector.Analysis
{
    public class RepoAnalyzer
    {
        private IRepoSource repoSource;
        public List<FileAnalysisResult> AnalysisResult { get; private set; }
        public RepoAnalyzer(IRepoSource repoSource)
        {
            this.repoSource = repoSource;
            AnalysisResult = new List<FileAnalysisResult>();
        }

        public void AnalyzeRepo()
        {
            foreach (var file in repoSource.GetFiles())
            {
                try
                {
                    var analyzer = new CodeAnalyzer(file);
                    AnalysisResult.Add(analyzer.AnalyzeCode());
                }
                catch (Esprima.ParserException e)
                {
                    var error = new ErrorResponse()
                    {
                        errorName = "PARSE_ERROR",
                        message = e.Description,
                        line = e.LineNumber,
                        column = e.Column
                    };
                    AnalysisResult.Add(new FileAnalysisResult() { error = error, FileName = file.fileName });
                }
            }
        }

    }
}