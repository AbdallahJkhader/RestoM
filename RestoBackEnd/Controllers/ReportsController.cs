using Microsoft.AspNetCore.Mvc;
using RestoBackEnd.Models;
using RestoBackEnd.Services;

namespace RestoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/Reports/dashboard
        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardStats>> GetDashboardStats()
        {
            var stats = await _reportService.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }
}
