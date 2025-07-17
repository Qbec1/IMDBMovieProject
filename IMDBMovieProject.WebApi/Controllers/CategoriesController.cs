using IMDBMovieProject.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class CategoriesController : Controller

    {
        private readonly DataBaseContext _context;

        public CategoriesController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Movies)
                .Include(c => c.News)
                .Include(c => c.ImdbTop100s)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
