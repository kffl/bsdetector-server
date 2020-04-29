using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Cors;

namespace BSDetector.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public class ErrorResponse
        {
            [JsonPropertyName("error")]
            public string errorName { get; set; }
            public string message { get; set; }
            public int line { get; set; }
            public int column { get; set; }
        }
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is Esprima.ParserException e)
            {
                var result = new JsonResult(
                    new ErrorResponse()
                    {
                        errorName = "PARSE_ERROR",
                        message = e.Description,
                        line = e.LineNumber,
                        column = e.Column
                    });
                result.StatusCode = 400;
                context.Result = result;
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class FileAnalysisController : ControllerBase
    {
        private Smell[] mockSmells = new Smell[2] {
            new TooManyParametersArrowFunction {
                Occurrences = new Occurrence[] {
                    new Occurrence {
                        Snippet = "cont l = 5;",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 0,
                        ColEnd = 12
                    },
                    new Occurrence {
                        Snippet = "var l = 10;\nvar m = 10;",
                        LineStart = 9,
                        LineEnd = 10,
                        ColStart = 0,
                        ColEnd = 11
                    },
            }.ToList()},
            new LineTooLong {
                Occurrences = new Occurrence[] {
                    new Occurrence {
                        Snippet = "function x(a, b, c, d, e, f) {",
                        LineStart = 5,
                        LineEnd = 5,
                        ColStart = 10,
                        ColEnd = 39
                    },
                    new Occurrence {
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
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public FileAnalysisResult Analyze([FromBody] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return analyzer.AnalyzeCode();
        }

        [HttpPost("/api/analyzemultipart")]
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public FileAnalysisResult AnalyzeMultipart([FromForm] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return analyzer.AnalyzeCode();
        }

        // Endpoint for development/debugging purposes
        // Builds an Abstract Syntax Tree without analysis
        [HttpPost("/api/ast")]
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public JsonStringResult GenerateAst([FromBody] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            //throw new ArgumentException("lol");
            return new JsonStringResult(analyzer.GetASTasJSONstring());
        }

        // Mock endpoint showing response structure
        [HttpPost("/api/analyzemock")]
        [EnableCors("ClientApp")]
        public FileAnalysisResult AnalyzeMock([FromBody] AnalyzeCodeResource data)
        {
            return new FileAnalysisResult
            {
                LinesAnalyzed = data.Code.Split('\n').Length,
                SmellsDetected = mockSmells
            };
        }
    }
}
