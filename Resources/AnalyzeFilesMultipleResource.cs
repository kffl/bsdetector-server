using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BSDetector.Resources
{
    public class AnalyzeFilesMultipleResource
    {
        [Required]
        [MaxLength(10)]
        public List<IFormFile> code { get; set; }
    }
}