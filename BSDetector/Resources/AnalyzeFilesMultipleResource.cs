using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BSDetector.Resources
{
    /// <summary>
    /// Represents a request containing one or multiple files uploaded for analysis
    /// </summary>
    public class AnalyzeFilesMultipleResource
    {
        [Required]
        [MaxLength(10)]
        public List<IFormFile> code { get; set; }
    }
}