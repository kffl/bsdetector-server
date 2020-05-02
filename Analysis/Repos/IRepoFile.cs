namespace BSDetector.Analysis.Repos
{
    public interface IRepoFile
    {
        public string fileName { get; }
        public string fileContent { get; }
    }
}