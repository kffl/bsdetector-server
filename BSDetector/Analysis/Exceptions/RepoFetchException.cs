namespace BSDetector.Analysis.Exceptions
{
    public class RepoFetchException : AnalysisException
    {
        public override int HTTPCode
        {
            get
            {
                return 404;
            }
        }
        public override string ErrorName
        {
            get
            {
                return "REPO_FETCH_ERROR";
            }
        }
        public override string Message
        {
            get
            {
                return "GitHub repo not found";
            }
        }
    }

}