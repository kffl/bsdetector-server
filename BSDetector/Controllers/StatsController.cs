using System.Threading.Tasks;
using BSDetector.Models;
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
        public async Task<Stat> Index()
        {
            return _context.Stats.Find("lines");
        }
    }
}