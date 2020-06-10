namespace BSDetector.Analysis.Repos
{
    /// <summary>
    /// Single file in repository source
    /// </summary>
    public interface IRepoFile
    {
        public string fileName { get; }
        public string fileContent { get; }
    }
}