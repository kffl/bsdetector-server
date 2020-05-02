using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BSDetector.Analysis.Repos;
using BSDetector.Analysis.Repos.GitHub;

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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var GithubApiRequestUrl = $"https://api.github.com/repos/{userName}/{repoName}/git/trees/master?recursive=true";
                    client.DefaultRequestHeaders.Add("User-Agent", "BSdetector");
                    using (HttpResponseMessage res = await client.GetAsync(GithubApiRequestUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var ghdata = await content.ReadAsStringAsync();
                            data = JsonSerializer.Deserialize<GitHubRepoTreeData>(ghdata);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public async Task FetchData()
        {
            //await Task.Run(() => this.BuildRepoTree());
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