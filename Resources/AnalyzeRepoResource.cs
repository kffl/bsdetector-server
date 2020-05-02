using System.ComponentModel.DataAnnotations;

namespace BSDetector
{
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

