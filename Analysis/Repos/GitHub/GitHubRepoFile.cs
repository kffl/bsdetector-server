using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSDetector.Analysis.Repos.GitHub
{
    public class GitHubRepoFile : IRepoFile
    {
        [JsonPropertyName("path")]
        public string fileName { get; set; }

        [JsonPropertyName("mode")]
        public string mode { get; set; }

        [JsonPropertyName("type")]
        public string objType { get; set; }

        [JsonPropertyName("sha")]
        public string SHA { get; set; }

        [JsonPropertyName("size")]
        public int size { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }

        public string fileContent { get; private set; }

        public bool isJsFile()
        {
            if (objType == "blob" && fileName.EndsWith(".js"))
            {
                if (!fileName.Contains("node_modules/") && !fileName.Contains(".min."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task FetchRawContent(string userName, string repoName)
        {
            using (HttpClient client = new HttpClient())
            {
                var GitHubRawRequestUrl = $"https://raw.githubusercontent.com/{userName}/{repoName}/master/{fileName}";
                client.DefaultRequestHeaders.Add("User-Agent", "BSdetector");
                using (HttpResponseMessage res = await client.GetAsync(GitHubRawRequestUrl))
                {
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        this.fileContent = data;
                    }
                }
            }
        }
    }
}