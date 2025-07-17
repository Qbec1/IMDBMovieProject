using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
using IMDBMovieProject.WebApi.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly DataBaseContext _context;

        public FavoritesController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler); // Model gönderildi
        }

        private List<Movies> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Movies>>("GetFavorites") ?? new List<Movies>();
        }

        public IActionResult Add(int MoviesId)
        {
            var favoriler = GetFavorites();
            var movies = _context.Movies.Find(MoviesId);

            if (movies != null && !favoriler.Any(p => p.Id == MoviesId))
            {
                favoriler.Add(movies);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int MoviesId)
        {
            var favoriler = GetFavorites();
            var updated = favoriler.Where(m => m.Id != MoviesId).ToList();
            HttpContext.Session.SetJson("GetFavorites", updated);
            return RedirectToAction("Index");
        }
    }
}