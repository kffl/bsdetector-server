using System.ComponentModel.DataAnnotations;

namespace BSDetector.Resources
{
    /// <summary>
    /// Represents a single file analysis request
    /// </summary>
    public class AnalyzeCodeResource
    {
        [Required]
        [MaxLength(200000)]
        public string Code { get; set; }
    }
}

