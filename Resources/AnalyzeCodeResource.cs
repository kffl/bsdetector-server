using System.ComponentModel.DataAnnotations;

namespace BSDetector
{
    public class AnalyzeCodeResource
    {
        [Required]
        [MaxLength(200000)]
        public string Code { get; set; }
    }
}

