using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeaTemperatureDashboard.Data;

namespace SeaTemperatureDashboard.Controllers
{
    [Authorize]
    public class SeaTemperaturesController : Controller
    {
        private readonly SeaTemperatureContext _context;

        public SeaTemperaturesController(SeaTemperatureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var seaTemperatures = await _context.SeaTemperatures.ToListAsync();
            return View(seaTemperatures);
        }
    }
}
