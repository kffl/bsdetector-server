using System.Collections.Generic;
using System.Threading.Tasks;
using BSDetector.Analysis;
using BSDetector.Repos.GitHub;
using BSDetector.Analysis.Exceptions;
using BSDetector.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BSDetector.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is Esprima.ParserException e)
            {
                var result = new JsonResult(
                    new ParseErrorResponse()
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
        // Uploaded files analysis endpoint
        // Allows for uploading multiple files at once
        [HttpPost("/api/analyzemultipart")]
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public FileAnalysisResult AnalyzeMultipart([FromForm] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            return analyzer.AnalyzeCode();
        }
        // Public github repo analysis endpoint
        [HttpPost("/api/analyzerepo")]
        [AnalysisExceptionFilter]
        [EnableCors("ClientApp")]
        public async Task<List<FileAnalysisResult>> AnalyzePublicRepo([FromBody] AnalyzeRepoResource data)
        {
            var repoSource = new GitHubRepoTree(data.username, data.reponame);
            await repoSource.FetchData();
            // Using dependency injection - giving RepoAnalyzer a preconfigured repo source
            var repoAnalyzer = new RepoAnalyzer(repoSource);
            repoAnalyzer.AnalyzeRepo();
            return repoAnalyzer.AnalysisResult;
        }
    }
}