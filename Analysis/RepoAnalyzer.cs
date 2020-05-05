using System.Collections.Generic;
using BSDetector.Analysis.Repos;
using BSDetector.Resources;

namespace BSDetector.Analysis
{
    /// <summary>
    /// Repository analyzer that analyzes code of given repo source
    /// </summary>
    public class RepoAnalyzer
    {
        private IRepoSource repoSource;
        /// <summary>
        /// Stores results of performed repo analysis
        /// </summary>
        /// <value>Repo analysis result</value>
        public List<FileAnalysisResult> AnalysisResult { get; private set; }
        /// <summary>
        /// RepoAnalyzer constructor that uses a pre-configured repository source via dependency injection
        /// </summary>
        /// <param name="repoSource">Pre-configured repository source for analysis</param>
        public RepoAnalyzer(IRepoSource repoSource)
        {
            this.repoSource = repoSource;
            AnalysisResult = new List<FileAnalysisResult>();
        }

        /// <summary>
        /// Performs code analysis of a repo
        /// </summary>
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
                    var error = new ParseErrorResponse()
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