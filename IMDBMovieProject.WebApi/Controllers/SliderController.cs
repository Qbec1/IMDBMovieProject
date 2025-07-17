using IMDBMovieProject.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class SliderController : Controller
    {
        private readonly DataBaseContext _context;

        public SliderController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(p => p.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }
    }
}
