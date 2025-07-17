using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class NewsController : Controller
    {
        private readonly DataBaseContext _context;

        public NewsController(DataBaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var news = _context.News.FirstOrDefault(n => n.Id == id);
            if (news == null) return NotFound();

            var relatedNews = _context.News
                                .Where(n => n.CategoryId == news.CategoryId && n.Id != id)
                                .Take(4)
                                .ToList();

            var viewModel = new NewsDetailViewModel
            {
                News = news,
                RelatedNews = relatedNews
            };

            return View(viewModel);
        }
    }
}
