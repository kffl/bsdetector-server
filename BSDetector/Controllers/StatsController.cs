using System.Threading.Tasks;
using BSDetector.Models;
using BSDetector.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSDetector.Controllers
{
    public class StatsController : Controller
    {
        private readonly StatsContext _context;

        public StatsController(StatsContext context)
        {
            _context = context;
        }

        [HttpGet("/api/stats")]
        [EnableCors("ClientApp")]
        public StatsResource Index()
        {
            var stats = new StatsResource
            {
                lines = _context.Stats.Find("lines").value,
                smells = _context.Stats.Find("smells").value,
                files = _context.Stats.Find("files").value,
                repos = _context.Stats.Find("repos").value
            };
            return stats;
        }
    }
}