using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSDetector.Analysis.Repos.GitHub
{
    /// <summary>
    /// Represents a file inside a public GitHub repository
    /// </summary>
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

        /// <summary>
        /// Determines whether or a file is a javascript source file for analysis
        /// </summary>
        /// <returns>Should this file be analyzed as JS file</returns>
        public bool isJsFile()
        {
            if (objType == "blob" && fileName.EndsWith(".js"))
            {
                Regex r = new Regex(@"node_modeules|\.min|\.spec|\.test|\.conf");
                if (!r.IsMatch(fileName))
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

        /// <summary>
        /// Fetches a file from raw GitHub user content service
        /// </summary>
        /// <param name="userName">Source repository owner's username</param>
        /// <param name="repoName">Source repository name</param>
        /// <returns></returns>
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