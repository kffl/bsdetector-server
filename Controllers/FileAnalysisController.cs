using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSDetector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileAnalysisController : ControllerBase
    {
        private FileAnalysisResult.Smell[] mockSmells = new FileAnalysisResult.Smell[2] {
            new FileAnalysisResult.Smell {
                Name="Sample_name",
                Description="Sample description of this smell",
                Occurances = new FileAnalysisResult.Occurance[] {
                    new FileAnalysisResult.Occurance {
                        Snippet = "cont l = 5;",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 0,
                        ColEnd = 12
                    },
                    new FileAnalysisResult.Occurance {
                        Snippet = "var l = 10;\nvar m = 10;",
                        LineStart = 9,
                        LineEnd = 10,
                        ColStart = 0,
                        ColEnd = 11
                    },
            }},
            new FileAnalysisResult.Smell {
                Name="Too many arguments for a function declaration",
                Description="A function declaration takes too many arguments. At most, 5 arguments per function are recommended.",
                Occurances = new FileAnalysisResult.Occurance[] {
                    new FileAnalysisResult.Occurance {
                        Snippet = "function x(a, b, c, d, e, f) {",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 10,
                        ColEnd = 39
                    },
                    new FileAnalysisResult.Occurance {
                        Snippet = "function qwerty(x, y, z, a, b, c) {",
                        LineStart = 9,
                        LineEnd = 9,
                        ColStart = 0,
                        ColEnd = 43
                    },
            }
            }};

        private readonly ILogger<FileAnalysisController> _logger;

        public FileAnalysisController(ILogger<FileAnalysisController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/api/analyze")]
        public FileAnalysisResult Analyze([FromBody] AnalyzeCodeResource data)
        {
            return new FileAnalysisResult {LinesAnalyzed = data.Code.Split('\n').Length,
                SmellsDetected=mockSmells};
        }

        [HttpPost("/api/analyzemultipart")]
        public FileAnalysisResult AnalyzeMultipart([FromForm] AnalyzeCodeResource data)
        {
            return new FileAnalysisResult {LinesAnalyzed = data.Code.Split('\n').Length,
                SmellsDetected=mockSmells};
        }
    }
}
