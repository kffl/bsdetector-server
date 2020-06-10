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
using BSDetector.Analysis.Repos.Uploaded;
using BSDetector.Models;

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
        private readonly StatsContext _context;
        public class JsonStringResult : ContentResult
        {
            public JsonStringResult(string json)
            {
                Content = json;
                ContentType = "application/json";
            }
        }

        private readonly ILogger<FileAnalysisController> _logger;

        public FileAnalysisController(ILogger<FileAnalysisController> logger, StatsContext context)
        {
            _logger = logger; _context = context;
        }

        private void UpdateStats(int lines, int smells, int files, int repos)
        {
            _context.AddToKeyAsync("lines", lines);
            _context.AddToKeyAsync("smells", smells);
            _context.AddToKeyAsync("files", files);
            _context.AddToKeyAsync("repos", repos);
        }

        private void UpdateStats(List<FileAnalysisResult> res)
        {
            int smells = 0, lines = 0;
            foreach (var file in res)
            {
                smells += file.SmellCount;
                lines += file.LinesAnalyzed;
            }

            UpdateStats(lines, smells, res.Count, 1);
        }

        [HttpPost("/api/analyze")]
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public FileAnalysisResult Analyze([FromBody] AnalyzeCodeResource data)
        {
            var analyzer = new CodeAnalyzer(data.Code);
            var res = analyzer.AnalyzeCode();
            UpdateStats(res.LinesAnalyzed, res.SmellCount, 1, 0);
            return res;
        }

        // Uploaded files analysis endpoint
        // Allows for uploading multiple files at once
        [HttpPost("/api/analyzemultipart")]
        [EnableCors("ClientApp")]
        [CustomExceptionFilter]
        public async Task<List<FileAnalysisResult>> AnalyzeMultipart([FromForm] AnalyzeFilesMultipleResource data)
        {
            var repoSource = new UploadedRepo();
            await repoSource.ReadUploadedFiles(data.code);
            var repoAnalyzer = new RepoAnalyzer(repoSource);
            repoAnalyzer.AnalyzeRepo();
            UpdateStats(repoAnalyzer.AnalysisResult);
            return repoAnalyzer.AnalysisResult;
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
            UpdateStats(repoAnalyzer.AnalysisResult);
            return repoAnalyzer.AnalysisResult;
        }
    }
}