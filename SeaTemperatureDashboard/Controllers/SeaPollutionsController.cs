using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeaTemperatureDashboard.Data;
using System.Threading.Tasks;

namespace SeaPollutionDashboard.Controllers
{
    [Authorize]
    public class SeaPollutionsController : Controller
    {
        private readonly SeaTemperatureContext _context;

        public SeaPollutionsController(SeaTemperatureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var seaPollutions = await _context.SeaPollutions.ToListAsync();

            if (startDate.HasValue && endDate.HasValue)
            {
                seaPollutions = seaPollutions.Where(p => p.DateRecorded >= startDate && p.DateRecorded <= endDate).ToList();
            }

            return Json(new { seaPollutions });
            var locations = seaPollutions.Select(p => p.Location).Distinct().ToList();

            ViewBag.Locations = locations;


            return View(seaPollutions);
        }
    }
}
