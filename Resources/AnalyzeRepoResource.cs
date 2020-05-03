using System.ComponentModel.DataAnnotations;

namespace BSDetector.Resources
{
    /// <summary>
    /// Represents a public GitHub repo analysis request
    /// </summary>
    public class AnalyzeRepoResource
    {
        [Required]
        [MaxLength(200)]
        public string username { get; set; }
        [Required]
        [MaxLength(200)]
        public string reponame { get; set; }
    }
}

