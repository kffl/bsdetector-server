using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BSDetector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileAnalysisController : ControllerBase
    {
        private Smell[] mockSmells = new Smell[2] {
            new TooManyParametersArrowFunction {
                Occurances = new Occurance[] {
                    new Occurance {
                        Snippet = "cont l = 5;",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 0,
                        ColEnd = 12
                    },
                    new Occurance {
                        Snippet = "var l = 10;\nvar m = 10;",
                        LineStart = 9,
                        LineEnd = 10,
                        ColStart = 0,
                        ColEnd = 11
                    },
            }.ToList()},
            new LineTooLong {
                Occurances = new Occurance[] {
                    new Occurance {
                        Snippet = "function x(a, b, c, d, e, f) {",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 10,
                        ColEnd = 39
                    },
                    new Occurance {
                        Snippet = "function qwerty(x, y, z, a, b, c) {",
                        LineStart = 9,
                        LineEnd = 9,
                        ColStart = 0,
                        ColEnd = 43
                    },
            }.ToList()
            }};

        public class JsonStringResult : ContentResult
        {
            public JsonStringResult(string json)
            {
                Content = json;
                ContentType = "application/json";
            }
        }

        private readonly ILogger<FileAnalysisController> _logger;

        public FileAnalysisController(ILogger<FileAnalysisController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/api/analyze")]
        public FileAnalysisResult Analyze([FromBody] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return analyzer.AnalyzeCode();
        }

        [HttpPost("/api/analyzemultipart")]
        public FileAnalysisResult AnalyzeMultipart([FromForm] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return analyzer.AnalyzeCode();
        }

        // Endpoint for development/debugging purposes
        // Builds an Abstract Syntax Tree without analysis
        [HttpPost("/api/ast")]
        public JsonStringResult GenerateAst([FromBody] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return new JsonStringResult(analyzer.GetASTasJSONstring());
        }

        // Mock endpoint showing response structure
        [HttpPost("/api/analyzemock")]
        public FileAnalysisResult AnalyzeMock([FromBody] AnalyzeCodeResource data)
        {
            return new FileAnalysisResult {LinesAnalyzed = data.Code.Split('\n').Length,
                SmellsDetected=mockSmells};
        }
    }
}
