using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class MovieController : Controller
    {
        private readonly DataBaseContext _context;

        public MovieController(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string  q="")
        {
            var dataBaseContext = _context.Movies.Where(p => p.IsActive && p.Title.Contains(q) || p.Description.Contains(q)).Include(p => p.Category);
            return View(await dataBaseContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }
            var model = new MovieDetailViewModel()
            {
                Movie = movies,
                RelatedMovies = _context.Movies.Where(p => p.IsActive && p.CategoryId == movies.CategoryId && p.Id != movies.Id)
            };  
            return View(model);
        }
    }
}
