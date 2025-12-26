using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IMDBMovieProject.WebApi.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly DataBaseContext _context;

        public FavoritesController(DataBaseContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                throw new Exception("Kullanıcı oturum bilgisi alınamadı.");
            }
            return userId;
        }

        public IActionResult Index()
        {
            int userId = GetCurrentUserId();

            var favorites = _context.Movies
                .FromSqlRaw(
                    "EXEC GetUserFavorites @UserId",
                    new SqlParameter("@UserId", userId)
                )
                .ToList();

            return View(favorites);
        }

        public IActionResult Add(int moviesId)
        {
            int userId = GetCurrentUserId();

            var existing = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.MovieId == moviesId);

            if (existing == null)
            {
                var favorite = new Favorite
                {
                    UserId = userId,
                    MovieId = moviesId
                };

                _context.Favorites.Add(favorite);
                _context.SaveChanges();

                return Redirect(Request.Headers["Referer"].ToString());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int moviesId)
        {
            int userId = GetCurrentUserId();

            var favorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.MovieId == moviesId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
