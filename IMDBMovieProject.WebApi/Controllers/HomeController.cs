using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseContext _context;

        public HomeController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            var movies = await _context.Movies.Where(p => p.IsActive && p.IsHome).ToListAsync();
            var news = await _context.News.ToListAsync();

            var model = new HomePageViewModel()
            {
                Sliders = sliders,
                Movies = movies,
                News = news
            };
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenided()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
