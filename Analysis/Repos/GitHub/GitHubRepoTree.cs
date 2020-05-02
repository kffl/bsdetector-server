using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BSDetector.Analysis.Repos;
using BSDetector.Analysis.Repos.GitHub;
using BSDetector.Analysis.Exceptions;

namespace BSDetector.Repos.GitHub
{
    public class GitHubRepoTree : IRepoSource
    {
        public class GitHubRepoTreeData
        {
            [JsonPropertyName("sha")]
            public string SHA { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("tree")]
            public List<GitHubRepoFile> Tree { get; set; }
        }

        private string userName;
        private string repoName;
        private GitHubRepoTreeData data;
        public GitHubRepoTree(string userName, string repoName)
        {
            this.userName = userName;
            this.repoName = repoName;
        }

        private async Task BuildRepoTree()
        {
            using (HttpClient client = new HttpClient())
            {
                var GithubApiRequestUrl = $"https://api.github.com/repos/{userName}/{repoName}/git/trees/master?recursive=true";
                client.DefaultRequestHeaders.Add("User-Agent", "BSdetector");
                using (HttpResponseMessage res = await client.GetAsync(GithubApiRequestUrl))
                {
                    if (!res.IsSuccessStatusCode)
                    {
                        throw new RepoFetchException();
                    }
                    using (HttpContent content = res.Content)
                    {
                        var ghdata = await content.ReadAsStringAsync();
                        data = JsonSerializer.Deserialize<GitHubRepoTreeData>(ghdata);
                    }
                }
            }
        }

        public async Task FetchData()
        {
            await BuildRepoTree();
            FilterFiles();
            await Task.WhenAll(data.Tree.Select(f => f.FetchRawContent(userName, repoName)));
        }

        private void FilterFiles()
        {
            List<GitHubRepoFile> filtered = new List<GitHubRepoFile>();
            foreach (var file in data.Tree)
            {
                if (file.isJsFile())
                {
                    filtered.Add(file);
                }
            }
            data.Tree = filtered;
        }

        public IEnumerable<IRepoFile> GetFiles()
        {
            return data.Tree;
        }
    }
}